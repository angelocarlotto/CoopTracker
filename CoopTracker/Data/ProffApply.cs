
using CoopTracker;
using CoopTracker.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;

namespace CoopTracker;
public static class ProffApplyExtension
{

    public static ProffApplyModelUpdate ToUpdateViewModel(this ProffApply proff)
    {
        var obj = new ProffApplyModelUpdate
        {
            Description = proff.Description,
            FileName = proff.FileName,
            FileType = proff.FileType,
            ProffApplyId = proff.ProffApplyId,
            TrackeeId = proff.TrackeeId,
            TenantId = proff.TenantId,
            UserPicture = "/ProffApply/FileEndpoint?id=" + proff.ProffApplyId.ToString()
        };

        var stream = new MemoryStream(proff.Image);

        // Create the FormFile object
        IFormFile formFile = new FormFile(stream, 0, proff.Image.Length, proff.FileName, proff.FileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = proff.FileType
        };


        obj.Image = formFile;
        return obj;
    }
    public static ProffApply ToUpdateEntity(this ProffApplyModelUpdate proff, ProffApply? entity)
    {
        var obj = new ProffApply
        {
            Description = proff.Description,
            FileName = proff.Image?.FileName,
            FileType = proff.Image?.ContentType,
            ProffApplyId = proff.ProffApplyId,
            TrackeeId = proff.TrackeeId,
            Image = new byte[0],
            TenantId = proff.TenantId
        };



        if (proff.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                proff.Image.CopyTo(memoryStream);
                // check the size of the file
                if (memoryStream.Length < 2097152)
                {
                    //ProffApplyId = someid,
                    obj.Image = memoryStream.ToArray();
                }
            }
        }
        return obj;
    }

    public static ProffApply ToUpdateEntity(this ProffApplyModelUpdate proff)
    {
        ProffApply obj = new ProffApply
        {
            Description = proff.Description,
            ProffApplyId = proff.ProffApplyId,
            TrackeeId = proff.TrackeeId,
            Image = new byte[0],
            TenantId = proff.TenantId
        };



        if (proff.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                proff.Image.CopyTo(memoryStream);
                // check the size of the file
                if (memoryStream.Length < 2097152)
                {
                    //ProffApplyId = someid,
                    obj.Image = memoryStream.ToArray();
                    obj.FileName = proff.Image?.FileName;
                    obj.FileType = proff.Image?.ContentType;
                }
            }
        }
        return obj;
    }


    public static ProffApplyModelCreate ToCreateViewModel(this ProffApply proff)
    {
        var obj = new ProffApplyModelCreate
        {
            Description = proff.Description,
            FileName = proff.FileName,
            FileType = proff.FileType,
            ProffApplyId = proff.ProffApplyId,
            TrackeeId = proff.TrackeeId,
            TenantId = proff.TenantId,
            UserPicture = "/ProffApply/FileEndpoint?id=" + proff.ProffApplyId.ToString()
        };

        var stream = new MemoryStream(proff.Image);

        // Create the FormFile object
        IFormFile formFile = new FormFile(stream, 0, proff.Image.Length, proff.FileName, proff.FileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = proff.FileType
        };


        obj.Image = formFile;
        return obj;
    }

    public static ProffApply ToCreateEntity(this ProffApplyModelCreate proff)
    {
        var obj = new ProffApply
        {
            Description = proff.Description,
            FileName = proff.Image.FileName,
            FileType = proff.Image.ContentType,
            ProffApplyId = proff.ProffApplyId,
            TrackeeId = proff.TrackeeId,
            Image = new byte[0],
            TenantId = proff.TenantId
        };

        using (var memoryStream = new MemoryStream())
        {
            proff.Image.CopyTo(memoryStream);
            // check the size of the file
            if (memoryStream.Length < 2097152)
            {
                //ProffApplyId = someid,
                obj.Image = memoryStream.ToArray();
            }
        }
        return obj;
    }
}

public class ProffApply : ITenantBaseEntity
{
    public required int ProffApplyId { get; set; }
    public Trackee? Trackee { get; set; }
    public required byte[] Image { get; set; }
    public required string Description { get; set; }
    public  string FileName { get; set; }
    public  string FileType { get; set; }
    public required int TrackeeId { get; set; }
    public required string TenantId { get; set; }

}
