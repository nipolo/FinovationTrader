using System.IO;

namespace FinovationTrader.Dtos.Responses
{
    public class GetAvatarImageResponseDto
    {
        public FileStream AvatarImage { get; set; }

        public string FileDownloadName { get; set; }
    }
}
