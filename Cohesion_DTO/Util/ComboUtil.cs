using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cohesion_DTO
{
    public class ComboUtil
    {
        public static Dictionary<string, List<string>> searchDic = new Dictionary<string, List<string>>();

        public static List<char> Answer = new List<char> { 'Y', 'N' };

        public static List<char> InspectUnit = new List<char> { 'N', 'C' };

        public static List<char> Store = new List<char> { };
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

   // 검사 데이터 같은 char 타입은 바인딩이 안되서 유기현이 추가함
   public class ComboCharConverter : CharConverter
   {
      public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
      {
         return true;
      }

      public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
      {
         return new StandardValuesCollection(ComboUtil.Answer);
      }
   }

    //검사 데이터 N / C (N일 경우 numeric 으로 규격값을 숫자를 입력 / C 일경우 Y / N 을 입력해야함.) 서지환 추가.
    public class ComboInspectUnitConverter : CharConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(ComboUtil.InspectUnit);
        }
    }

    public class ComboStoreConverter : CharConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(ComboUtil.Store);
        }//STOCK_IN_FLAG
    }
}
