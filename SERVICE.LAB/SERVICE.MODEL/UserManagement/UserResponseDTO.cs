using System;
using System.Collections.Generic;

namespace DEV.Model
{
    public partial class UserRequestEntity
    {      
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int LoginType { get; set; }
        public string Ipaddress { get; set; }
        public int VenueBranchNo { get; set; }
        public string ClientSysteminfo { get; set; }

    }
    public partial class UserResponseEntity
    {
        public int ResponseStatus { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string? DomainCode { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public bool? IsAdmin { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool? IsProvisional { get; set; }
        public string menuNos { get; set; }
        public bool? IsEditLabResults { get; set; }
        public List<UserMenuCodeDTO> lstMenuCode { get; set; }
        public List<UserRoleNameDTO> lstUserRoleName { get; set; }
        public List<string> UserRoleNames { get; set; }
        public bool? IsResultEntryHIV { get; set; }
        public bool? IsResultEntryVIP { get; set; }
        public bool? IsPriceShow { get; set; }
        public bool? isLock { get; set; }
        public string VenueBranchName { get; set; }
        public bool? IsAbnormalAvail { get; set; }
        public bool? IsEditGrn { get; set; }
        public bool? IsPOApproval { get; set; }
        public bool? IsGrnApproval { get; set; }
        public bool? IsGrnReturnApproval { get; set; }
        public bool? IsStockAdjustmentApproval { get; set; }
        public bool? IsConsumptionApproval { get; set; }
        public bool? IsClientApproval { get; set; }
    }
    public class ChangePasswordEntity
    {
        public int UserNo { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int usertype { get; set; }
        public int changeUserNo { get; set; }

    }
    public partial class UserClaimsIdentity : Shared.User
    {

    }
    public partial class UserDetailsDTO
    {
        public Int64 Row_Num { get; set; }
        public int TotalRecords { get; set; }
        public int PageIndex { get; set; }
        public int UserNo { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string Email { get; set; }
        public string? PhoneNo { get; set; }
        public bool IsLogin { get; set; }
        public bool IsRider { get; set; }
        public bool IsMarketing { get; set; }
        public bool IsMobile { get; set; }
        public bool Status { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int Createdby { get; set; }
        public String deptJson { get; set; }
        public bool EditPatient { get; set; }
        public bool DueReport { get; set; }
        public string dashBoardDetailsJson { get; set; }
        public String branchJson { get; set; }
        public string? DefaultBranch { get; set; }
        public int Discount { get; set; }
        public int roleId { get; set; }
        public string? roleName { get; set; }
        public string dob { get; set; }
        public string doj { get; set; }
        public string dor { get; set; }
        public bool IsEditResults { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsResultsEntryVIP { get; set; }
        public bool IsResultsEntryHIV { get; set; }
        public bool IsPathologist { get; set; }
        public bool isLock { get; set; }
        public bool isPriceShow { get; set; }
        public bool IsadmultifactorAccess { get; set; }
        public bool Isadaccess { get; set; }
        public bool? IsEditGrn { get; set; }
        public bool? IsPOApproval { get; set; }
        public bool? IsGrnApproval { get; set; }
        public bool? IsGrnReturnApproval { get; set; }
        public bool? IsStockTransferApproval { get; set; }
        public bool? IsStockAdjustmentApproval { get; set; }
        public bool? IsConsumptionApproval { get; set; }
        public bool? IsEditPO { get; set; }
        public bool? IsEditGrnReturn { get; set; }
        public bool? IsEditStockTransfer { get; set; }
        public bool? IsEditStockAdjustment { get; set; }
        public bool? IsEditConsumption { get; set; }
        public string DefaultBranchNo { get; set; }
        public bool? IsApproveClient { get; set; }
        public bool? IsProvisional { get; set; }
        public bool? SuppressSCDtTm { get; set; }
    }
    public partial class UserResponseDTO
    {
        public int UserNo { get; set; }
    }
    public partial class UserSessionResponseDTO
    {
        public int status { get; set; }
    }


    public partial class UserMenuMappingDTO
    {
        public Int64 Row_Num { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int TaskMasterid { get; set; }
        public string TaskName { get; set; }
        public bool MenuStatus { get; set; }
        public bool TaskStatus { get; set; }
    }
    public partial class UserMenuTaskDTO
    {
        public Int64 Row_Num { get; set; }
        public string MenuCode { get; set; }
        public string Taskcode { get; set; }
        public string TaskName { get; set; }
        public bool TaskStatus { get; set; }
    }

    public partial class UserModuleDTO
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool Status { get; set; }
        public List<UserMenuDTO> UserMenuItem { get; set; }
    }
    public partial class UserMenuDTO
    {
        public int MenuId { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public bool Status { get; set; }
        public List<UserTaskDTO> TaskItem { get; set; }
    }
    public partial class UserTaskDTO
    {
        public int TaskNo { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public bool Status { get; set; }
    }
    public class ReqUserMenu
    {
        public int modifiedBy { get; set; }
        public int createdBy { get; set; }
        public int userNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public List<usermenudata> usermenuitem { get; set; }
        public int MenuUserNo { get; set; }
    }
    public partial class usermenudata
    {
        public int menuno { get; set; }
        public int taskNo { get; set; }    
    }
    public partial class userbranchlist
    {
        public Int64 Row_Num { get; set; }
        public int VenueBranchNo { get; set; }
        public string VenueBranchName { get; set; }
        public bool Isdefault { get; set; }
    }
    public class ReqRoleMenu
    {
        public int modifiedBy { get; set; }
        public int createdBy { get; set; }
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int RoleId { get; set; }
        public List<usermenudata> usermenuitem { get; set; }
    }
    public partial class RoleResponseDTO
    {
        public int RoleId { get; set; }
        public int UserNo { get; set; }
        public bool Status { get; set; }

    }
    public partial class RolegetReqDTO
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int RoleId { get; set; }
    }
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
    public partial class ActionMenuNoResponseDTO
    {
        public int Result { get; set; }
    }
    public partial class ActionMenuCodeResponseDTO
    {
        public int Result { get; set; }
    }

    public partial class UserMenuNosResponseDTO
    {
        public string menuNos { get; set; }
    }

    public class GetRoleReq
    {
        public int RoleId { get; set; }
        public int VenueNo { get; set; }
        public int pageIndex { get; set; }

    }
    public partial class GetRoleRes
    {
        public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
    public partial class InsertRoleReq
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
    public partial class InsertRoleRes
    {
        public int RoleId { get; set; }
        public string message { get; set; }
    }

    public class Branch
    {
        public int branchNo { get; set; }
        public string branchName { get; set; }
        public bool IsChecked { get; set; }
        public bool Isdefault { get; set; }
    }
    public partial class UserDepartmentDTO
    {
        public int? venueNo { get; set; }
        public int? venueBranchNo { get; set; }
        public int? userNo { get; set; }
    }
    public partial class GetUserDepartmentDTO
    {
        public int? rowNo { get; set; }
        public int? deptNo { get; set; }
        public string? deptName { get; set; }
        public int? userNo { get; set; }
    }
}