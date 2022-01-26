namespace MVCAppointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddDoctors",
                c => new
                    {
                        DID = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(),
                        specialization = c.Int(),
                        RoomNo = c.Int(nullable: false),
                        PhoneNo = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.DID)
                .ForeignKey("dbo.Specializations", t => t.specialization)
                .Index(t => t.specialization);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Specname = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ptid = c.Int(nullable: false),
                        specid = c.Int(),
                        docid = c.Int(nullable: false),
                        dt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AddDoctors", t => t.docid, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.ptid, cascadeDelete: true)
                .ForeignKey("dbo.Specializations", t => t.specid)
                .Index(t => t.ptid)
                .Index(t => t.specid)
                .Index(t => t.docid);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNo = c.String(),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        RefName = c.String(),
                        RefPhoneNo = c.String(),
                        RefRelation = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DID = c.Int(nullable: false),
                        TimeSlot = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AddDoctors", t => t.DID, cascadeDelete: true)
                .Index(t => t.DID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "DID", "dbo.AddDoctors");
            DropForeignKey("dbo.Appointments", "specid", "dbo.Specializations");
            DropForeignKey("dbo.Appointments", "ptid", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "docid", "dbo.AddDoctors");
            DropForeignKey("dbo.AddDoctors", "specialization", "dbo.Specializations");
            DropIndex("dbo.Doctors", new[] { "DID" });
            DropIndex("dbo.Appointments", new[] { "docid" });
            DropIndex("dbo.Appointments", new[] { "specid" });
            DropIndex("dbo.Appointments", new[] { "ptid" });
            DropIndex("dbo.AddDoctors", new[] { "specialization" });
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
            DropTable("dbo.Specializations");
            DropTable("dbo.AddDoctors");
        }
    }
}
