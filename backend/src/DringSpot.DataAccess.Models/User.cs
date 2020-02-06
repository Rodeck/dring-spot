using System;
using System.Collections.Generic;
using System.Linq;

namespace DringSpot.DataAccess.Models
{
    public class User 
    {
        public User() 
        {
            Friends = new List<Friend>();
            FavouredPlaces = new List<FavouredPlace>();
        }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Email { get; set; }

        public string Uid { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }

        public virtual ICollection<FavouredPlace> FavouredPlaces { get; set; }

        public virtual ICollection<FriendsInvitation> Invitations { get; set; }

        public virtual ICollection<MyInvitation> MyInvitations { get; set; }

        public void AddInvitation(string inviterId)
        {
            if (Friends.Any(x => x.FriendId == inviterId))
                throw new Exception("Cannot invite, already friends.");
            
            if (Invitations.Any(x => x.InviterId == inviterId))
                throw new Exception("Invitation already exists.");

            Invitations.Add(new FriendsInvitation() {
                Icon = "fa-handshake",
                InvitationDate = DateTime.Now,
                InviterId = inviterId
            });
        }

        public void AddMyInvitation(string invitedId)
        {
            MyInvitations.Add(new MyInvitation() {
                InvitationDate = DateTime.Now,
                InvitedId = invitedId,
            });
        }
    }
}