using static System.Net.Mime.MediaTypeNames;

namespace SH1ProjeUygulamasi.WebUI.Tools
{
	public class FileHelper
	{
		public static string FileLoader(IFormFile formFile) //resim dosyası yükleme işlemi için kod azaltmak için yeni metot tanımladık
		{
			string dosyaAdi = "";

			dosyaAdi = formFile.FileName; //geri döndürülen değere dosya adı eşitlendi
			string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/";
			using var stream = new FileStream(klasor + formFile.FileName, FileMode.Create); //yeni dosya olarak yükle
			formFile.CopyTo(stream);

			return dosyaAdi;
		}
	}
}