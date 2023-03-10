

using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.InterfaceService.InterfacesBase;

namespace UC.InterfaceService.ExternalInterfaces
{
    public interface ISmsService 
    {
        int GenerateActiveCode();
        Task<Sms> Send(string SenderMobile, string ReceptorMobile, string ActiveCode, string TextMessage);
        Task<string> DeliveredByMobile(string ReceptorMobile);
        Task<string> DeliveredByMessageID(string MessageID);
        Task<bool> Delete(string ReceptorMobile);
        Task Insert(Sms t);
        Task<bool> FindByActiveCode(string ReceptorMobile, string ActiveCode);
    }
}
