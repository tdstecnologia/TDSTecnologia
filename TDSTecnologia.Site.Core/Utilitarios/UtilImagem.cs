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
        public static async Task<byte[]> ConvertarParaByte(IFormFile imagem)
        {
            if (imagem != null && imagem.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                await imagem.OpenReadStream().CopyToAsync(ms);
                return ms.ToArray();
            }
            return null;
        }
    }
}
