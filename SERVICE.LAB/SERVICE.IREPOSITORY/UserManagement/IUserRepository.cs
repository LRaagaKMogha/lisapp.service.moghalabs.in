using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace Dev.IRepository
{
    public interface IUserRepository
    {
        UserResponseEntity UserLogIn(UserRequestEntity req, IJWTManagerRepository jWTManager);
        UserResponseEntity ValidateOTP(UserRequestEntity req, IJWTManagerRepository _jWTManagerRepository);
        int ResetSecret(UserRequestEntity req);
        List<UserDetailsDTO> GetUserDetails(int VenueNo, int VenueBranchNo, int PageIndex, int DefaultBranchNo);
        int InsertUserMaster(UserDetailsDTO Useritem);
        List<UserModuleDTO> GetUserMenuMapping(int VenueNo, int VenueBranchNo, int userno, int MenuLoadUserNo);
        int InsertMenuMapping(ReqUserMenu Useritem);
        List<NavDTO> GetPageMenuList(int VenueNo, int VenueBranchNo, int userno, int logintype);
        int ChangePassword(ChangePasswordEntity req);
        int ResetPassword(int UserNo, int venueNo, int VenueBranchNo, int usertype, int ResetUserNo);
        List<UserMenuDTO> GetUserTask(int VenueNo, int VenueBranchNo, int userno);
        List<UserDashBoardMasterResponse> GetDashBoardMaster(int CustomerNo, int VenueNo, int VenueBranchNo);
        List<userbranchlist> GetUserBranchList(int userno, int VenueNo, int VenueBranchNo);
        int InsertRoleMenuMapping(ReqRoleMenu Useritem);
        List<UserModuleDTO> GetRoleMenuMapping(RolegetReqDTO rolereq);
        int ValidateActionMenu(int UserNo, int VenueNo, string MenuType);
        bool ValidateUserSession(int userno, int VenueNo, int VenueBranchNo, string token);
        void UpdateSession(int userno, int VenueNo, int VenueBranchNo, string token);
        bool ValidateUserMenuCode(int UserNo, int VenueNo, int VenueBranchNo, string menuCode);
        List<UserRoleNameDTO> GetUserMenuCode(int UserNo, int VenueNo, int VenueBranchNo, int logintype);
        List<GetRoleRes> GetRoleMaster(GetRoleReq req);
        InsertRoleRes InsertRoleMaster(InsertRoleReq req);
        int sendOTP(int userno, string phoneno);
        int verifyOTP(int userno, string otptext);
        UserResponseEntity UserESign(UserRequestEntity req);
        List<GetUserDepartmentDTO> GetUserDepartment(UserDepartmentDTO deptreq);
    }
}

