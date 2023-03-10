
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UC.Common.Enum;
using System.Security.Cryptography;
using UC.DataLayer.Contex;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using UC.ClassDomain.Domains;
using System.Net;
using Microsoft.AspNetCore.Routing;
using UC.InterfaceService.InterfacesBase;

namespace UC.Common.Authorization
{
#pragma warning disable
    public class AuthorizeCommon : Attribute, IAsyncAuthorizationFilter
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext _HttpContext)
        {
            using (var Context_UC = new ContextUC())
            {
                _UnitOfWorkUCService = _HttpContext.HttpContext.RequestServices.GetService<IUnitOfWorkUCService>();
                bool _State = false; string _Token = string.Empty; var User = new Token();
                if (_HttpContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    _Token = _HttpContext.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                    if (_Token != "")
                    {
                        string[] _TokenMain = _Token.Split(" ");
                        string _TokenHash = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenMain[1]);
                        User = await Context_UC.Token.FirstOrDefaultAsync(x => x.Token_HashCode == _TokenHash).ConfigureAwait(false);
                        if (User != null)
                        {
                            var Def_Date = Math.Round(DateTime.Now.Subtract(User.Token_DateExpire).TotalMinutes);
                            if (Def_Date <= 60) // 60
                                _State = true;
                            else
                            {
                                var Def_LastDate = Math.Round(DateTime.Now.Subtract(User.Token_DateLastAccessTime).TotalMinutes);
                                if (Def_LastDate <= 60) // 60
                                {
                                    User.Token_DateLastAccessTime = DateTime.Now;
                                    Context_UC.Entry<Token>(User).State = EntityState.Modified;
                                    await Context_UC.SaveChangesAsync().ConfigureAwait(false);
                                    _State = true;
                                }
                                else
                                {
                                    if (User != null && User.Token_UserID > 0)
                                    {
                                        Context_UC.Token.Remove(User);
                                        await Context_UC.SaveChangesAsync().ConfigureAwait(false);
                                    }
                                }
                            }
                        }
                    }
                    if (!_State)
                    {
                        _HttpContext.Result = new JsonResult("Error")
                        {
                            Value = new
                            {
                                Status = "401",
                                Message = "Unauthorized"
                            },
                        };
                        return;
                    }
                    else
                        return;
                }
                _HttpContext.Result = new JsonResult("Error")
                {
                    Value = new
                    {
                        Status = "401",
                        Message = "Unauthorized"
                    },
                };
                return;
            }
        }
    }
}
