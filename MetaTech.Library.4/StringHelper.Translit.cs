#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion
using System.Collections;

namespace MetaTech.Library
{
  public partial class StringHelper
  {
    #region Latin
    public static string Latin(string str)
    {
      str = str.Replace('à', 'a');
      str = str.Replace('á', 'a');
      str = str.Replace('â', 'a');
      str = str.Replace('ã', 'a');
      str = str.Replace('ä', 'a');
      str = str.Replace('å', 'a');
      str = str.Replace('è', 'e');
      str = str.Replace('é', 'e');
      str = str.Replace('ê', 'e');
      str = str.Replace('ë', 'e');
      str = str.Replace('ì', 'i');
      str = str.Replace('í', 'i');
      str = str.Replace('î', 'i');
      str = str.Replace('ï', 'i');
      str = str.Replace('ò', 'o');
      str = str.Replace('ó', 'o');
      str = str.Replace('ô', 'o');
      str = str.Replace('õ', 'o');
      str = str.Replace('ö', 'o');

      str = str.Replace('ù', 'u');
      str = str.Replace('ú', 'u');
      str = str.Replace('û', 'u');
      str = str.Replace('ü', 'u');

      str = str.Replace('À', 'A');
      str = str.Replace('Á', 'A');
      str = str.Replace('Â', 'A');
      str = str.Replace('Ã', 'A');
      str = str.Replace('Ä', 'A');
      str = str.Replace('Å', 'A');
      str = str.Replace('È', 'E');
      str = str.Replace('É', 'E');
      str = str.Replace('Ê', 'E');
      str = str.Replace('Ë', 'E');
      str = str.Replace('Ì', 'I');
      str = str.Replace('Í', 'I');
      str = str.Replace('Î', 'I');
      str = str.Replace('Ï', 'I');
      str = str.Replace('Ò', 'O');
      str = str.Replace('Ó', 'O');
      str = str.Replace('Ô', 'O');
      str = str.Replace('Õ', 'O');
      str = str.Replace('Ö', 'O');
      str = str.Replace('Ù', 'U');
      str = str.Replace('Ú', 'U');
      str = str.Replace('Û', 'U');
      str = str.Replace('Ü', 'U');
      return str;
  }
      #endregion

    #region Translit
    /// <summary>
    /// Очень не оптимальная
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Translit(string str)
    {
      str = str.Replace("а", "a");
      str = str.Replace("б", "b");
      str = str.Replace("в", "v");
      str = str.Replace("г", "g");
      str = str.Replace("д", "d");
      str = str.Replace("е", "e");
      str = str.Replace("ё", "e");
      str = str.Replace("ж", "zh");
      str = str.Replace("з", "z");
      str = str.Replace("и", "i");
      str = str.Replace("й", "y");
      str = str.Replace("к", "k");
      str = str.Replace("л", "l");
      str = str.Replace("м", "m");
      str = str.Replace("н", "n");
      str = str.Replace("о", "o");
      str = str.Replace("п", "p");
      str = str.Replace("р", "r");
      str = str.Replace("с", "s");
      str = str.Replace("т", "t");
      str = str.Replace("у", "u");
      str = str.Replace("ф", "f");
      str = str.Replace("х", "kh");
      str = str.Replace("ц", "ts");
      str = str.Replace("ч", "ch");
      str = str.Replace("ш", "sh");
      str = str.Replace("щ", "sch");
      str = str.Replace("ъ", "`");
      str = str.Replace("ы", "y");
      str = str.Replace("ь", "`");
      str = str.Replace("э", "e");
      str = str.Replace("ю", "yu");
      str = str.Replace("я", "ya");
      str = str.Replace("А", "A");
      str = str.Replace("Б", "B");
      str = str.Replace("В", "V");
      str = str.Replace("Г", "G");
      str = str.Replace("Д", "D");
      str = str.Replace("Е", "E");
      str = str.Replace("Ё", "E");
      str = str.Replace("Ж", "Zh");
      str = str.Replace("З", "Z");
      str = str.Replace("И", "I");
      str = str.Replace("Й", "Y");
      str = str.Replace("К", "K");
      str = str.Replace("Л", "L");
      str = str.Replace("М", "M");
      str = str.Replace("Н", "N");
      str = str.Replace("О", "O");
      str = str.Replace("П", "P");
      str = str.Replace("Р", "R");
      str = str.Replace("С", "S");
      str = str.Replace("Т", "T");
      str = str.Replace("У", "U");
      str = str.Replace("Ф", "F");
      str = str.Replace("Х", "Kh");
      str = str.Replace("Ц", "Ts");
      str = str.Replace("Ч", "Ch");
      str = str.Replace("Ш", "Sh");
      str = str.Replace("Щ", "Sch");
      str = str.Replace("Ъ", "`");
      str = str.Replace("Ы", "Y");
      str = str.Replace("Ь", "`");
      str = str.Replace("Э", "E");
      str = str.Replace("Ю", "Yu");
      str = str.Replace("Я", "Ya");
      return str;
  }
  #endregion

  }
}
