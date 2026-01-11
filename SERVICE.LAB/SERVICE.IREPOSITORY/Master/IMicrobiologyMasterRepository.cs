using System;
using System.Collections.Generic;
using System.Text;
using Service.Model;

namespace Dev.IRepository
{
    public interface IMicrobiologyMasterRepository
    {

        List<lstorgAntiRange> GetOrgAntibioticRange(reqorgAntiRange req);
        int SaveOrganismAntibioticRange(orgAntiRange req);
        List<orggetresponse> GetOrgmaster(reqorgAntiRange orggetreq);
        List<orgGrpresponse> GetOrgGrpmaster(reqorgGroupAntiRange orggetreq);
        orginsertresponse InsertOrgmaster(orgresponse orginsertreq);
        orginsertGrpresponse InsertOrgGrpmaster(orggrpresponse orginsertreq);
        List<orgtyperesponse> GetOrgtypemaster(orgtypereq orgtygetreq);
        orgtypeinsertresponse InsertOrgtypemaster(orgtyperesponse orgtyinsertreq);
        List<antiresponse> GetAntimaster(antireq antireq);
        antinsertresponse Insertantimaster(antiresponse antinsertreq);
        List<orgAntiresponse> GetorgAntimaster(orgAntirequest reqorgAnti);
        organtinsertresponse InsertorgAntimaster(orgAntinsertresponse orgAntinsertreq);

    }

}
