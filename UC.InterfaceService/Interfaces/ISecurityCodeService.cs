using System.Drawing;
using UC.ClassDTO.DTOs;

namespace UC.Interface.Interfaces
{
    public interface ISecurityCodeService
    {
        int GenarateSecurityCode();
        string CreateImageSecurityCode(int SecurityCode);
        string CreateTokenSecurityCode(int SecurityCode, DtoSettingToken DtoSettingToken);
        bool IValidSecurityCode(string SecurityCodeUser, string SecurityCodeToken);
        Bitmap ImpulseNoise(Bitmap bm);
    }
}
