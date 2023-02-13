using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cohesion_DTO;


namespace Cohesion_Project.Util
{
    public class ComboUtil
    {
        public static Dictionary<string, List<string>> searchDic = new Dictionary<string, List<string>>();
        public ComboUtil()
        {
            List<CODE_TABLE_MST_DTO> tableList = new Service.Srv_CommonData().SelectCommonTable();
            List<CODE_DATA_MST_DTO> dataList = new Service.Srv_CommonData().SelectAllCommonTableData();

            for (int i = 0; i < tableList.Count; i++)
            {
                List<string> l1 = new List<string>();
                string tableName = tableList[i].CODE_TABLE_NAME;
                dataList.FindAll((c) => c.CODE_TABLE_NAME.Equals(tableName)).ForEach((c) => l1.Add(c.KEY_1));
                searchDic[tableName] = l1;
            }
            Cohesion_DTO.ComboUtil.searchDic = searchDic;
        }
    }

    public class ComboStringConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(ComboUtil.searchDic[context.PropertyDescriptor.Description]);
        }
    }
}
