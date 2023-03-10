

using AutoMapper;
using Kavenegar.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.Common.Mapping;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.InterfaceService.ExternalInterfaces;
using UC.Service.ServiceBase;

namespace UC.Service.ExternalServices
{
    public class SmsService : ISmsService
    {
        private readonly ContextUC _ContextUC;
        IMapper _IMapper;
        private readonly string _ApiKeySms = "3055714552656C3266667446617061494A32417A444F69512F31596664526A586F746858434A5A47715A6B3D";
        Kavenegar.KavenegarApi _ApiSms;

        public SmsService(ContextUC ContextUC)
        {
            _ContextUC = ContextUC;
            _IMapper = MapperSms.MapTo();
            _ApiSms = new Kavenegar.KavenegarApi(_ApiKeySms);
        }

        public int GenerateActiveCode()
        {
            int _Min = 10000; int _Max = 99999;
            var _Random = new Random();
            return _Random.Next(_Min, _Max);
        }

        public async Task<Sms> Send(string SenderMobile, string ReceptorMobile, string ActiveCode, string TextMessage)
        {
            var _SendResult = new SendResult();
            Sms _Sms = new Sms();
            try
            {
                _SendResult = await _ApiSms.VerifyLookup(ReceptorMobile, ActiveCode, TextMessage).ConfigureAwait(false);
                _Sms.Sms_Status = _SendResult.Status;
            }
            catch (Exception ex)
            {
                // var _ex = ex.Message;
                _Sms.Sms_Status = 400;
            }
            //var _SendResult = await _ApiSms.VerifyLookup(ReceptorMobile, ActiveCode, TextMessage).ConfigureAwait(false);
            _Sms.Sms_MessageID = _SendResult.Messageid.ToString();
            _Sms.Sms_Mobile = ReceptorMobile; // _SendResult.Receptor;
            _Sms.Sms_ActiveCode = ActiveCode;
            _Sms.Sms_IsActive = true;
            _Sms.Sms_Time = DateTime.Now;
            return _Sms;
        }

        public async Task<string> DeliveredByMessageID(string MessageID)
        {
            var _SendResult = await _ApiSms.Status(MessageID).ConfigureAwait(false);
            return _SendResult.Status.ToString();
        }

        public async Task<string> DeliveredByMobile(string ReceptorMobile)
        {
            var _Sms = (await _ContextUC.Sms.FirstOrDefaultAsync(S => S.Sms_Mobile == ReceptorMobile).ConfigureAwait(false));
            if (_Sms == null) return "";
            var _SendResult = await _ApiSms.Status(_Sms.Sms_MessageID).ConfigureAwait(false);
            return _SendResult.Status.ToString();
        }

        public async Task<bool> FindByActiveCode(string ReceptorMobile, string ActiveCode)
        {
            var _SmsActiveCode = await _ContextUC.Sms.Where(S => S.Sms_Mobile == ReceptorMobile && S.Sms_ActiveCode == ActiveCode).FirstOrDefaultAsync().ConfigureAwait(false);
            if (_SmsActiveCode == null) return false;
            var _DeffTimeSms = DateTime.Now - _SmsActiveCode.Sms_Time;
            if (_DeffTimeSms.Minutes > 2) return false;
            return true;
        }

        public async Task Insert(Sms t)
        {
            _ContextUC.ChangeTracker.Clear();
            await _ContextUC.Sms.AddAsync(t).ConfigureAwait(false);
        }

        public async Task<bool> Delete(string ReceptorMobile)
        {
            if (ReceptorMobile == "") return false;
            var _Sms = await _ContextUC.Sms.Where(S => S.Sms_Mobile == ReceptorMobile).ToListAsync().ConfigureAwait(false);
            if (_Sms == null) return false;
            _ContextUC.ChangeTracker.Clear();
            _ContextUC.RemoveRange(_Sms);
            return true;
        }

    }
}