
 
using UC.ClassDTO.DTOs;
using UC.Interface.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp;

namespace UC.Service.Services
{
    public class SecurityCodeService : ISecurityCodeService
    {

        public SecurityCodeService()
        {

        }

        public int GenarateSecurityCode()
        {
            int Min = 10000;
            int Max = 99999;
            Random _SecCode = new Random();
            return _SecCode.Next(Min, Max);
        }


        public string CreateImageSecurityCode(int securitycode)
        {

            string str = "";
            string _securitycode = securitycode.ToString();
            string fontname = "Verdana";
            int fontsize = 16;

            int width = 200;
            int height = 50;

            //Load the fonts
            SixLabors.Fonts.FontCollection fonts = new SixLabors.Fonts.FontCollection();
            SixLabors.Fonts.FontFamily font1 = SixLabors.Fonts.SystemFonts.Get(fontname);

            SixLabors.Fonts.Font font = font1.CreateFont(fontsize, SixLabors.Fonts.FontStyle.Regular);

            using var image = new SixLabors.ImageSharp.Image<Rgba32>(width, height);
            image.Mutate(imageContext =>
            {
                // Fill the canvas with a background color
                imageContext.Fill(SixLabors.ImageSharp.Color.FromRgb(230, 239, 255));

                // Create an area to draw your text.
                var textRectangle = new SixLabors.ImageSharp.RectangleF(80, 15, 200, 50);

                // Fill the text rectangle so we can see the region on the output image.
                imageContext.Fill(SixLabors.ImageSharp.Color.FromRgb(230, 239, 255), textRectangle);

                // Draw the text onto the canvas.
                var pointF = new SixLabors.ImageSharp.PointF(textRectangle.X, textRectangle.Y);

                imageContext.DrawText(securitycode.ToString(), font, SixLabors.ImageSharp.Color.FromRgb(0, 0, 0), pointF);


                MemoryStream _MS = new MemoryStream();
                image.SaveAsJpeg(_MS);
                byte[] byteImage = _MS.ToArray();

                str = Convert.ToBase64String(byteImage);

            });

            return str;

        }

        /*
        public string CreateImageSecurityCode(int SecurityCode)
        {
            string _SecurityCode = SecurityCode.ToString();
            string FontName = "Tahoma";
            int FontSize = 16;
            Color FontColor = Color.FromArgb(0, 89, 255);
            Color BGColor = Color.FromArgb(230, 239, 255);
            int width = 200;
            int Height = 50;


            Bitmap bitmap = new Bitmap(width, Height);
            Font Font = new Font(FontName, FontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            Graphics graphics = Graphics.FromImage(bitmap);
            int _width = (int)graphics.MeasureString(_SecurityCode, Font).Width;
            int _height = (int)graphics.MeasureString(_SecurityCode, Font).Height;
            bitmap = new Bitmap(bitmap);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(BGColor);
            graphics.Transform.Rotate(80);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;


            var x = (width - _width) / 2;
            var y = (Height - _height) / 2;


            graphics.DrawString(_SecurityCode, Font, new SolidBrush(Color.FromArgb(0, 89, 255)), x, y);

            graphics.Flush();
            graphics.Dispose();

            //  bitmap = ImpulseNoise(bitmap);


            MemoryStream _MS = new MemoryStream();
            bitmap.Save(_MS, ImageFormat.Jpeg);
            byte[] byteImage = _MS.ToArray();
            return Convert.ToBase64String(byteImage);


        }
        */

        public  string CreateTokenSecurityCode(int SecurityCode, DtoSettingToken DtoSettingToken)
        {
            var _Claims = new List<Claim>();
            _Claims.Add(new Claim(JwtRegisteredClaimNames.Sub, DtoSettingToken.Subject));
            _Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, DtoSettingToken.Guid.ToString()));
            _Claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DtoSettingToken.DateTime_UtcNow.ToString()));
            _Claims.Add(new Claim("SecurityCode", SecurityCode.ToString()));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DtoSettingToken.Signing_Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var _Token = new JwtSecurityToken(
                                    DtoSettingToken.Issuer,
                                    DtoSettingToken.Audience,
                                    _Claims,
                                    expires: DtoSettingToken.DateTime_Now.AddMinutes(DtoSettingToken.Expir_Minutes),
                                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

      
        public bool IValidSecurityCode(string SecurityCodeUser, string SecurityCodeToken)
        {
            if (SecurityCodeUser == SecurityCodeToken) return true;
            return false;
        }

        public Bitmap ImpulseNoise(Bitmap bm)
        {
            throw new NotImplementedException();
        }
    }
}
