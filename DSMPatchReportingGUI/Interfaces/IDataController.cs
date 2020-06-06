using DSMPatchReportingGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Interfaces
{
    interface IDataController
    {
        List<PatchData> GetPatches(string BaseURL, JWT jwt);
        List<DateOpenClosedStatDto> GetDateOpenClosedStats(string BaseURL, JWT jwt, DataFilter filter = null);
        List<PatchCountOfComplianceDto> GetMostSecurityIssues(string BaseURL, JWT jwt, DataFilter filter = null);

        List<PatchData> GetPatchDataByDate(string BaseURL, JWT jwt, DataFilter filter = null);

        List<ComputerOpenClosedStatDto> GetFixedInPercent(string BaseURL, JWT jwt, DataFilter filter = null);
        List<PatchData> GetLatestSecurityIssues(string BaseURL, JWT jwt, DataFilter filter = null);
        List<String> GetComputers(string BaseURL, JWT jwt);
        JWT Login(string BaseURL, UserForLoginDto user);
        List<String> GetUsers(string BaseURL, JWT jwt);
        bool DeleteUser(string BaseURL, JWT jwt, string username);
        String GetLastError();
        bool RegisterUser(string BaseURL, JWT jwt, UserForRegisterDto userForRegisterDto);
        bool ChangePassword(string BaseURL, JWT jwt, UserForRegisterDto userForRegisterDto, string password);
    }
}
