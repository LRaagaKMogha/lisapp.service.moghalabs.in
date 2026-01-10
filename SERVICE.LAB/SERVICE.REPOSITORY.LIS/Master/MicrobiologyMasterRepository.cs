using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;
using Serilog;
using Newtonsoft.Json;
using System.IO;

namespace Dev.Repository
{
    public class MicrobiologyMasterRepository : IMicrobiologyMasterRepository
    {
        private IConfiguration _config;
        public MicrobiologyMasterRepository(IConfiguration config) { _config = config; }


        public List<lstorgAntiRange> GetOrgAntibioticRange(reqorgAntiRange req)
        {
            List<lstorgAntiRange> lst = new List<lstorgAntiRange>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.venuebranchno);
                    var _organismno = new SqlParameter("organismno", req?.organismno);
                    var _organismtypeno = new SqlParameter("organismtypeno", req?.OrganismGroupNo);

                    lst = context.GetOrgAntibioticRange.FromSqlRaw(
                      "Execute dbo.pro_GetOrgAntibioticRangeMaster @venueno,@venuebranchno,@organismno,@organismtypeno",
                      _venueno, _venuebranchno, _organismno, _organismtypeno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.GetOrgAntibioticRange" + req.organismno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, 0);
            }
            return lst;
        }

        public int SaveOrganismAntibioticRange(orgAntiRange req)
        {
            CommonHelper commonUtility = new CommonHelper();
            string orgAntiRangeXML = commonUtility.ToXML(req.lstorgAntiRange);
            int i = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", req?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", req?.venuebranchno);
                    var _userno = new SqlParameter("userno", req?.userno);
                    var _orgAntiRangeXML = new SqlParameter("orgAntiRangeXML", orgAntiRangeXML);

                    var lst = context.SaveOrganismAntibioticRange.FromSqlRaw(
                       "Execute dbo.pro_InsertOrganismAntibioticRange @venueno,@venuebranchno,@userno,@orgAntiRangeXML",
                      _venueno, _venuebranchno, _userno, _orgAntiRangeXML).ToList();

                    i = lst[0].testNo;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.SaveOrganismAntibioticRange", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueno, req.venuebranchno, req.userno);
            }
            return i;
        }

        public List<orggetresponse> GetOrgmaster(reqorgAntiRange orggetreq)
        {
            List<orggetresponse> lst = new List<orggetresponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orggetreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orggetreq?.venuebranchno);
                    var _organismno = new SqlParameter("organismno", orggetreq?.organismno);
                    var _OrganismGroupNo = new SqlParameter("OrganismGroupNo", orggetreq?.OrganismGroupNo);
                    var _pageIndex = new SqlParameter("pageIndex", orggetreq?.pageIndex);

                    lst = context.GetOrgmaster.FromSqlRaw(
                      "Execute dbo.pro_GetOrganismmaster @venueno,@venuebranchno,@organismno,@OrganismGroupNo,@pageIndex",
                      _venueno, _venuebranchno, _organismno, _OrganismGroupNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.GetOrgmaster" + orggetreq.organismno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orggetreq.venueno, orggetreq.venuebranchno, 0);
            }
            return lst;
        }
        public List<orgGrpresponse> GetOrgGrpmaster(reqorgGroupAntiRange orggetreq)
        {
            List<orgGrpresponse> lst = new List<orgGrpresponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orggetreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orggetreq?.venuebranchno);
                    var _organismgrpno = new SqlParameter("organismgrpno", orggetreq?.organismGrpno);
                    var _organismtypeno = new SqlParameter("organismtypeno", orggetreq?.organismtypeno);
                    var _pageIndex = new SqlParameter("pageIndex", orggetreq?.pageIndex);

                    lst = context.GetOrgGrpmaster.FromSqlRaw(
                      "Execute dbo.pro_GetOrganismGroupMaster @venueno,@venuebranchno,@organismgrpno,@organismtypeno,@pageIndex",
                      _venueno, _venuebranchno, _organismgrpno, _organismtypeno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.GetOrgGrpmaster" + orggetreq.organismGrpno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orggetreq.venueno, orggetreq.venuebranchno, 0);
            }
            return lst;
        }


        public orginsertresponse InsertOrgmaster(orgresponse orginsertreq)
        {
            orginsertresponse objresult = new orginsertresponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orginsertreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orginsertreq?.venuebranchno);
                    var _organismno = new SqlParameter("organismno", orginsertreq?.organismno);
                    var _organismGroupno = new SqlParameter("organismGroupno", orginsertreq?.organismgroupno);
                    var _organismname = new SqlParameter("organismname", orginsertreq?.organismname);
                    var _notes = new SqlParameter("notes", orginsertreq?.notes);
                    var _sequenceno = new SqlParameter("sequenceNo", orginsertreq?.sequenceno);
                    var _status = new SqlParameter("status", orginsertreq?.status);
                    var _userno = new SqlParameter("userNo", orginsertreq?.userno);
                    var _organismshortcode = new SqlParameter("organismshortcode", orginsertreq?.organismshortcode);

                    objresult = context.InsertOrgmaster.FromSqlRaw(
                      "Execute dbo.pro_InsertOrganismmaster @venueno,@venuebranchno,@organismno,@organismGroupno,@organismname,@notes,@sequenceno,@status,@userno,@organismshortcode",
                      _venueno, _venuebranchno, _organismno, _organismGroupno, _organismname, _notes, _sequenceno, _status, _userno, _organismshortcode).AsEnumerable().FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.InsertOrgmaster" + orginsertreq.organismno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orginsertreq.venueno, orginsertreq.venuebranchno, orginsertreq.userno);
            }
            return objresult;
        }
        public orginsertGrpresponse InsertOrgGrpmaster(orggrpresponse orginsertreq)
        {
            orginsertGrpresponse objresult = new orginsertGrpresponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orginsertreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orginsertreq?.venuebranchno);
                    var _organismgrpno = new SqlParameter("organismgrpno", orginsertreq?.organismgrpno);
                    var _organismgrpname = new SqlParameter("organismgrpname", orginsertreq?.organismgrpname);
                    var _organismtypeno = new SqlParameter("organismtypeno", orginsertreq?.organismtypeno);
                    var _sequenceno = new SqlParameter("sequenceNo", orginsertreq?.sequenceno);
                    var _status = new SqlParameter("status", orginsertreq?.status);
                    var _userno = new SqlParameter("userNo", orginsertreq?.userno);

                    var lst = context.InsertOrgGrpmaster.FromSqlRaw(
                      "Execute dbo.pro_InsertOrganismGrpmaster @venueno,@venuebranchno,@organismgrpno,@organismgrpname,@organismtypeno,@sequenceno,@status,@userNo ",
                      _venueno, _venuebranchno, _organismgrpno, _organismtypeno, _organismgrpname, _sequenceno, _status, _userno).ToList();
                    objresult.organismGrpno = lst[0].organismGrpno;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.InsertOrgGrpmaster" + orginsertreq.organismgrpno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orginsertreq.venueno, orginsertreq.venuebranchno, orginsertreq.userno);
            }
            return objresult;
        }
        public List<orgtyperesponse> GetOrgtypemaster(orgtypereq orgtygetreq)
        {
            List<orgtyperesponse> lst = new List<orgtyperesponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orgtygetreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orgtygetreq?.venuebranchno);
                    var _organismtypeno = new SqlParameter("organismtypeno", orgtygetreq?.organismtypeno);
                    var _pageIndex = new SqlParameter("pageIndex", orgtygetreq?.pageIndex);

                    lst = context.GetOrgtypemaster.FromSqlRaw(
                      "Execute dbo.pro_GetOrganismTypemaster @venueno,@venuebranchno,@organismtypeno,@pageIndex",
                      _venueno, _venuebranchno, _organismtypeno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.GetOrgtypemaster" + orgtygetreq.organismtypeno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orgtygetreq.venueno, orgtygetreq.venuebranchno, 0);
            }
            return lst;
        }
        public orgtypeinsertresponse InsertOrgtypemaster(orgtyperesponse orgtyinsertreq)
        {
            orgtypeinsertresponse objresult = new orgtypeinsertresponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orgtyinsertreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orgtyinsertreq?.venuebranchno);
                    var _organismtypeno = new SqlParameter("organismtypeno", orgtyinsertreq?.organismtypeno);
                    var _organismtypename = new SqlParameter("organismtypename", orgtyinsertreq?.organismTypeName);
                    var _sequenceno = new SqlParameter("sequenceno", orgtyinsertreq?.sequenceno);
                    var _status = new SqlParameter("status", orgtyinsertreq?.status);
                    var _userno = new SqlParameter("userno", orgtyinsertreq?.userno);

                    objresult = context.InsertOrgtypemaster.FromSqlRaw(
                      "Execute dbo.pro_InsertOrganismTypemaster @venueno,@venuebranchno,@organismtypeno,@organismtypename,@sequenceno,@status,@userNo",
                      _venueno, _venuebranchno, _organismtypeno, _organismtypename, _sequenceno, _status, _userno).AsEnumerable().FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.InsertOrgtypemaster" + orgtyinsertreq.organismtypeno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orgtyinsertreq.venueno, orgtyinsertreq.venuebranchno, orgtyinsertreq.userno);
            }
            return objresult;
        }
        public List<antiresponse> GetAntimaster(antireq antireq)
        {
            List<antiresponse> lst = new List<antiresponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", antireq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", antireq?.venuebranchno);
                    var _antibioticno = new SqlParameter("antibioticno", antireq?.antibioticno);
                    var _pageIndex = new SqlParameter("pageIndex", antireq?.pageIndex);

                    lst = context.Getantibiotic.FromSqlRaw(
                      "Execute dbo.pro_GetAntibioticmaster @venueno,@venuebranchno,@antibioticno,@pageIndex",
                      _venueno, _venuebranchno, _antibioticno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.GetAntimaster" + antireq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, antireq.venueno, antireq.venuebranchno, 0);
            }
            return lst;
        }
        public antinsertresponse Insertantimaster(antiresponse antinsertreq)
        {
            antinsertresponse objresult = new antinsertresponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", antinsertreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", antinsertreq?.venuebranchno);
                    var _antibioticno = new SqlParameter("antibioticno", antinsertreq?.antibioticno);
                    var _antibioticName = new SqlParameter("antibioticName", antinsertreq?.antibioticName);
                    var _sequenceno = new SqlParameter("sequenceno", antinsertreq?.sequenceno);
                    var _status = new SqlParameter("status", antinsertreq?.status);
                    var _userno = new SqlParameter("userno", antinsertreq?.userno);

                    objresult = context.Insertantimaster.FromSqlRaw(
                      "Execute dbo.pro_InsertAntibioticsmaster @venueno,@venuebranchno,@antibioticno,@antibioticName,@sequenceno,@status,@userNo",
                      _venueno, _venuebranchno, _antibioticno, _antibioticName, _sequenceno, _status, _userno).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.Insertantimaster" + antinsertreq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, antinsertreq.venueno, antinsertreq.venuebranchno, antinsertreq.userno);
            }
            return objresult;
        }
        public List<orgAntiresponse> GetorgAntimaster(orgAntirequest reqorgAnti)
        {
            List<orgAntiresponse> lst = new List<orgAntiresponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", reqorgAnti?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", reqorgAnti?.venuebranchno);
                    var _organismAntibioticMapNo = new SqlParameter("organismAntibioticMapNo", reqorgAnti?.organismAntibioticMapNo);
                    var _organismTypeNo = new SqlParameter("organismTypeNo", reqorgAnti?.organismTypeNo);
                    var _antibioticno = new SqlParameter("antibioticno", reqorgAnti?.antibioticno);
                    var _pageIndex = new SqlParameter("pageIndex", reqorgAnti?.pageIndex);
                    var _organismNo = new SqlParameter("organismNo", reqorgAnti?.organismNo);

                    lst = context.Getantirog.FromSqlRaw(
                      "Execute dbo.pro_GetOrganismtypeandantibioticmaster @venueno,@venuebranchno,@organismAntibioticMapNo,@organismTypeNo,@antibioticno,@pageIndex,@organismNo",
                      _venueno, _venuebranchno, _organismAntibioticMapNo, _organismTypeNo, _antibioticno, _pageIndex, _organismNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.GetorgAntimaster" + reqorgAnti.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, reqorgAnti.venueno, reqorgAnti.venuebranchno, 0);
            }
            return lst;
        }
        public organtinsertresponse InsertorgAntimaster(orgAntinsertresponse orgAntinsertreq)
        {
            organtinsertresponse objresult = new organtinsertresponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueno = new SqlParameter("venueno", orgAntinsertreq?.venueno);
                    var _venuebranchno = new SqlParameter("venuebranchno", orgAntinsertreq?.venuebranchno);
                    var _organismAntibioticMapNo = new SqlParameter("organismAntibioticMapNo", orgAntinsertreq?.organismAntibioticMapNo);
                    var _organismTypeNo = new SqlParameter("organismTypeNo", orgAntinsertreq?.organismTypeNo);
                    var _antibioticno = new SqlParameter("antibioticno", orgAntinsertreq?.antibioticno);
                    var _sequenceno = new SqlParameter("sequenceno", orgAntinsertreq?.sequenceno);
                    var _status = new SqlParameter("status", orgAntinsertreq?.status);
                    var _userno = new SqlParameter("userno", orgAntinsertreq?.userno);
                    var _organismNo = new SqlParameter("organismNo", orgAntinsertreq?.organismNo);

                    objresult = context.Insertantiorg.FromSqlRaw(
                      "Execute dbo.pro_InsertOrganismtypeandantibioticmaster @venueno,@venuebranchno,@organismAntibioticMapNo,@organismTypeNo,@antibioticno,@organismNo,@sequenceno,@status,@userno",
                      _venueno, _venuebranchno, _organismAntibioticMapNo, _organismTypeNo, _antibioticno, _organismNo, _sequenceno, _status, _userno).AsEnumerable().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "MicrobiologyMasterRepository.InsertorgAntimaster" + orgAntinsertreq.antibioticno.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, orgAntinsertreq.venueno, orgAntinsertreq.venuebranchno, orgAntinsertreq.userno);
            }
            return objresult;
        }
    }
}
