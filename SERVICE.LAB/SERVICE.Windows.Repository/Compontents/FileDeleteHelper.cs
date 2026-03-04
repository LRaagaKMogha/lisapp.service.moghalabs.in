using Service.Win.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Service.Win.Repository
{
    public class FileDeleteHelper
    {
        public void FileDeleteService()
        {
            try
            {
                List<tbl_ReportMaster> lst = GetFileDeleteList();
                if (lst.Count > 0)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string FolderPath = lst[i].ExportPath;
                        if (Directory.Exists(FolderPath))
                        {
                            var day = lst[i].UnDeletedFileDays;
                            var IsDelete = lst[i].IsDelete;
                            if (IsDelete == true)
                            {
                                DirectoryInfo SourceFolder = new DirectoryInfo(FolderPath);
                                if (day == 0)
                                {
                                    foreach (FileInfo SourceFile in SourceFolder.GetFiles())
                                    {
                                        SourceFile.Delete();
                                    }
                                }
                                else
                                {
                                    foreach (FileInfo SourceFile in SourceFolder.GetFiles())
                                    {
                                        if (SourceFile.LastWriteTime <= DateTime.Now.AddDays(-(int)day))
                                            SourceFile.Delete();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
        }

        public List<tbl_ReportMaster> GetFileDeleteList()
        {
            List<tbl_ReportMaster> objresult = new List<tbl_ReportMaster>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.tbl_ReportMaster.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.ToString());
            }
            return objresult;
        }
    }
}
