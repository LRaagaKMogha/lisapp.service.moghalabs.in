using System.Collections.Generic;
using Service.Model;

namespace Service.IRepository
{
    public interface IMicrobiologyMasterRepository
    {
        List<LstorgAntiRange> GetOrgAntibioticRange(reqorgAntiRange req);
        int SaveOrganismAntibioticRange(orgAntiRange req);
        List<Orggetresponse> GetOrgmaster(reqorgAntiRange orggetreq);
        List<Orggrpresponse> GetOrgGrpmaster(reqorgGroupAntiRange orggetreq);
        Orginsertresponse InsertOrgmaster(Orgresponse orginsertreq);
        OrginsertGrpresponse InsertOrgGrpmaster(Orggrpresponse orginsertreq);
        List<Orgtyperesponse> GetOrgtypemaster(Orgtypereq orgtygetreq);
        Orgtypeinsertresponse InsertOrgtypemaster(Orgtyperesponse orgtyinsertreq);
        List<Antiresponse> GetAntimaster(Antireq Antireq);
        Antinsertresponse Insertantimaster(Antiresponse antinsertreq);
        List<OrgAntiresponse> GetorgAntimaster(OrgAntirequest reqorgAnti);
        OrgAntiInsertResponse InsertorgAntimaster(OrgAntinsertresponse orgAntinsertreq);
    }
}