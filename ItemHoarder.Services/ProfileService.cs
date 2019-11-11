using ItemHoarder.Data;
using ItemHoarder.Models;
using ItemHoarder.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Services
{
    public class ProfileService
    {
        private readonly Guid _userId;
        public ProfileService(Guid userId)
        {
            _userId = userId;
        }
        //get profile info
        public ProfileInfo GetMyProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var profile = ctx.UserProfile.Include("Photo").SingleOrDefault(e => e.UserID == _userId);
                var info = new ProfileInfo
                {
                    UserID = profile.UserID,
                    Username = profile.Username,
                    ProfileImage = (profile.Photo.Count > 0) ? new PhotoDisplay
                    {
                        PhotoID = profile.Photo.First().PhotoID,
                        PhotoName = profile.Photo.First().PhotoName,
                        FileType = profile.Photo.First().FileType,
                        ContentType = profile.Photo.First().ContentType,
                        Content = profile.Photo.First().Content
                    } : null,
                    About = profile.About,
                    DateOfCreation = profile.DateOfCreation,
                    DateOfModification = profile.DateOfModification
                };
                return info;
            }
        }
        //get other persons info
        public ProfileInfo GetProfileInfoByUsername(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var profile = ctx.UserProfile.Include("Photo").SingleOrDefault(e => e.Username == username);
                return new ProfileInfo
                {
                    Username = profile.Username,
                    ProfileImage = new PhotoDisplay
                    {
                        PhotoID = profile.Photo.First().PhotoID,
                        PhotoName = profile.Photo.First().PhotoName,
                        FileType = profile.Photo.First().FileType,
                        ContentType = profile.Photo.First().ContentType,
                        Content = profile.Photo.First().Content
                    },
                    About = profile.About,
                    DateOfCreation = profile.DateOfCreation
                };
            }
        }
        //create profile info
        public bool CreateProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.SingleOrDefault(e => e.Id == _userId.ToString());
                if (ctx.UserProfile.SingleOrDefault(e => e.UserID == _userId) == null)
                {
                    var newProfile = new Profile
                    {
                        UserID = _userId,
                        Username = user.UserName,
                        //Photo = null,
                        About = null,
                        DateOfCreation = DateTimeOffset.UtcNow,
                        DateOfModification = DateTimeOffset.UtcNow
                    };
                    ctx.UserProfile.Add(newProfile);
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //update profile info
        public bool UpdateProfileInfo(ProfileUpdate profile)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldProfile = ctx.UserProfile.Include("Photo").SingleOrDefault(e => e.UserID == _userId);
                if (oldProfile != null)
                {
                    if (profile.PhotoUpload != null && profile.PhotoUpload.ContentLength > 0)
                    {
                        var photo = new Photo
                        {
                            PhotoName = System.IO.Path.GetFileName(profile.PhotoUpload.FileName),
                            FileType = FileType.Profile,
                            ContentType = profile.PhotoUpload.ContentType,
                            DateOfCreation = DateTimeOffset.UtcNow
                        };
                        using (var reader = new System.IO.BinaryReader(profile.PhotoUpload.InputStream))
                        {
                            photo.Content = reader.ReadBytes(profile.PhotoUpload.ContentLength);
                        }
                        if (oldProfile.Photo.Count > 0)
                        {
                            oldProfile.Photo.Clear();
                        }
                        oldProfile.Photo = new List<Photo> { photo };
                    }
                    oldProfile.About = profile.About;
                    oldProfile.DateOfModification = DateTimeOffset.UtcNow;
                    var save = ctx.SaveChanges();
                    if (save == 3 || save == 4)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
        }

        //delete profile info
        public bool DeleteProfileInfo()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var profile = ctx.UserProfile.Single(e => e.UserID == _userId);
                ctx.UserProfile.Remove(profile);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
