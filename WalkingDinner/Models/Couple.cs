using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkingDinner.Models {

    public class Couple : DatabaseRecord<Couple> {

        public Dinner Dinner { get; set; }
        public int DinnerID { get; }

        [Required]
        public PersonMain PersonMain { get; set; }

        public PersonGuest PersonGuest { get; set; }

        [Required]
        [Display( Name = "Emailadres" )]
        public string EmailAddress { get; set; }

        [DataType( DataType.MultilineText )]
        [Display( Name = "Dieetwensen" )]
        public string DietaryGuidelines { get; set; }

        [DataType( DataType.PhoneNumber )]
        [Display( Name = "Telefoonnummer" )]
        public string PhoneNumber { get; set; }

        [Display( Name = "Rekeningnummer" )]
        public string IBAN { get; set; }

        public CoupleAddress Address { get; set; }

        public bool Accepted { get; set; }

        public string PaymentId { get; set; }

        public bool Validated { get; set; }

        public bool IsAdmin { get; set; }

        public string AdminCode { get; set; }

        [NotMapped]
        public bool Registered { get => Accepted && Validated; }

        public override void CopyFrom( Couple source ) {

            if ( source == null ) {
                return;
            }

            PersonMain.CopyFrom( source.PersonMain );
            if ( PersonGuest != null ) {

                if ( source.PersonGuest != null ) {

                    PersonGuest.CopyFrom( source.PersonGuest );
                } else {

                    PersonGuest = null;
                }
            } else {
                PersonGuest = source.PersonGuest;
            }

            EmailAddress        = source.EmailAddress;
            DietaryGuidelines   = source.DietaryGuidelines;
            PhoneNumber         = source.PhoneNumber;
            IBAN                = source.IBAN;

            if ( Address != null ) {

                if ( source.Address != null ) {

                    Address.CopyFrom( source.Address );
                } else {

                    Address = null;
                }
            } else {
                Address = source.Address;
            }
        }

        public string GetName() {

            string result = PersonMain.FirstName;
            if ( PersonGuest != null ) {
                result += $" en { PersonGuest.FirstName }";
            }

            return result;
        }
    }
}
