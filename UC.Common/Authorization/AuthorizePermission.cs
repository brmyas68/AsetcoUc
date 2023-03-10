
using UC.Common.Enum;
using UC.DataLayer.Contex;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using UC.ClassDomain.Domains;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using UC.InterfaceService.InterfacesBase;
using UC.Common.StoredProcedure;

namespace UC.Common.Authorization
{
#pragma warning disable
    public class AuthorizePermission : Attribute, IAsyncAuthorizationFilter
    {
        private EnumPermission.Controllers? _Controller;
        private EnumPermission.Actions? _Actions;
        private IUnitOfWorkUCService _UnitOfWorkUCService;
        private IConfiguration _Configuration;
        public AuthorizePermission(EnumPermission.Controllers Controller, EnumPermission.Actions Action)
        {
            _Controller = Controller; _Actions = Action;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext _HttpContext)
        {
            using (var Context_UC = new ContextUC())
            {
                _UnitOfWorkUCService = _HttpContext.HttpContext.RequestServices.GetService<IUnitOfWorkUCService>();
                _Configuration = _HttpContext.HttpContext.RequestServices.GetService<IConfiguration>();
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
                    if (_State)
                    {
                        var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                        var _CountFound = (await _UnitOfWorkUCService._IAuthenticationService.CheckPermissions(User.Token_UserID, TenantID, _Controller.ToString(), _Actions.ToString()));
                        if (_CountFound > 0)
                        {
                            _State = true; return;
                        }
                        else _State = false;
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
