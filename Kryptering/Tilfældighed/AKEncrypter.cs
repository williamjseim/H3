public class AKEncrypter{

    public static char[] a = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ".ToLower().ToCharArray();
    public static char[] K = "KLMNOPQRSTUVWXYZÆØÅABCDEFGHIJ".ToLower().ToCharArray();

    public static string Encrypt(string text){
        var chars = text.ToLower().ToCharArray();
        string output = "";
        for (int i = 0; i < chars.Length; i++)
        {
            try{
                
            int j = Array.IndexOf(a, chars[i]);
            output += K[j];
            }
            catch{
                if(chars[i] == ' '){
                    output+= ' ';
                }
            }
        }
        return output;
    }

    public static string DeCrypt(string text){
        var chars = text.ToLower().ToCharArray();
        string output = "";
        for (int i = 0; i < chars.Length; i++)
        {
            try{
                
            int j = Array.IndexOf(K, chars[i]);
            output += a[j];
            }
            catch{
                if(chars[i] == ' '){
                    output+= ' ';
                }
                if(chars[i] == ','){
                    output+= ',';
                }
                if(chars[i] == '.'){
                    output+= '.';
                }
            }
        }
        return output;
    }

}