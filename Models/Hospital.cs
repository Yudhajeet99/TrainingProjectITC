using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace MVCAppointment.Models
{

    public class AddDoctor
    {
        [Key]
        public int DID { get; set; }
        public string DoctorName { get; set; }
        [ForeignKey("spec")]
        public int? specialization { get; set; }
        public Specialization spec { get; set; }
        public int RoomNo { get; set; }
        public string PhoneNo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Gender { get; set; }
    }
    public class Patient
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string RefName { get; set; }
        public string RefPhoneNo { get; set; }
        public string RefRelation { get; set; }
    }
    public class Doctor
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("docid")]
        public int DID { get; set; }
        public AddDoctor docid { get; set; }
        public DateTime TimeSlot { get; set; }
    }
    public class Specialization
    {
        public int id { get; set; }
        public string Specname { get; set; }
    }

    public class Appointment
    {
        public int id { get; set; }
        [ForeignKey("pt")]
        public int ptid { get; set; }
        public Patient pt { get; set; }
        [ForeignKey("spec")]
        public int? specid { get; set; }
        public Specialization spec { get; set; }
        [ForeignKey("doc")]
        public int docid { get; set; }
        public AddDoctor doc { get; set; }
        public DateTime sTime { get; set; }
        public DateTime eTime { get; set; }

    }
    public class HospitalContext : DbContext
    {
        public DbSet<AddDoctor> AddDoctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
    }
}