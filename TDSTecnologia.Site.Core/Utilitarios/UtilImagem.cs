using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TDSTecnologia.Site.Core.Utilitarios
{
    public class UtilImagem
    {
        public static async Task<byte[]> ConverterParaByte(IFormFile imagem)
        {
            if (imagem != null && imagem.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                await imagem.OpenReadStream().CopyToAsync(ms);
                return ms.ToArray();
            }
            return null;
        }

        public static string ConverterByteArrayParaStringBase64(byte[] imagem)
        {
           return imagem != null ? "data:image/png;base64," + Convert.ToBase64String(imagem, 0, imagem.Length) : null;
        }
    }
}
