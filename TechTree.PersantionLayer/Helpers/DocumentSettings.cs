using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.Helpers
{
    public static class DocumentSettings
    {
        // I Formfile => becuase it will be UpLoade From Form HTml
        public static string UploadFile(IFormFile formFile, string folderName)
        {
            // 1. Get Directory Of Folder Path . 
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            // 2. Get FileName And make this NameOfFile unique.[Through Guid] .
            string fileName = $"{Guid.NewGuid()} {Path.GetFileName(formFile.FileName)}";

            // 3. Get File Path [Directory Of Folder + File Name] .
            string filePath = Path.Combine(FolderPath, fileName);

            // 4. Upload This File .
            using var fileStream = new FileStream(filePath, FileMode.Create);

            // Here it will Take Copy From IFormFile at FileStream . 
            formFile.CopyTo(fileStream);

            //This File Name That Will Be Stored in DataBase . 
            return fileName;
        }


        public static void DeletedFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                @"wwwroot\Files", folderName, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }


    public class CompareUsers : IEqualityComparer<ApplicationUser>
    {
        public bool Equals(ApplicationUser? x, ApplicationUser? y)
        {
            if (y is null)
                return false;

            if (x.Id == y.Id)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] ApplicationUser obj) => obj.Id.GetHashCode();
    }


    public class CompareCategories: IEqualityComparer<Category>
    {
        public bool Equals(Category? x, Category? y)
        {
            if (y is null)
                return false;

            if (x.Id == y.Id)
                return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Category obj) => obj.Id.GetHashCode();
    }

}
