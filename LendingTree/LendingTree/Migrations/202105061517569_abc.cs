namespace LendingTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        AgentId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Password = c.String(nullable: false),
                        NoOfApplications = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgentId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        LoanAmount = c.Double(nullable: false),
                        DueTime = c.Int(nullable: false),
                        Status = c.String(),
                        Income = c.Double(nullable: false),
                        LoanType = c.String(nullable: false),
                        PANNo = c.String(),
                        AdhaarNo = c.Long(nullable: false),
                        BankAccountNo = c.Long(nullable: false),
                        FK_PhysicalVerificationAgent = c.String(maxLength: 128),
                        FK_ApprovalAgencyAgent = c.String(maxLength: 128),
                        FK_PickupAgent = c.String(maxLength: 128),
                        FK_LegalAgent = c.String(maxLength: 128),
                        FK_User = c.String(maxLength: 128),
                        Approved = c.Boolean(nullable: false),
                        PhysicallyVerified = c.Boolean(nullable: false),
                        VerifiedByPickUpAgent = c.Boolean(nullable: false),
                        VerifiedByLegalAgent = c.Boolean(nullable: false),
                        Sanctioned = c.Boolean(nullable: false),
                        Agent_AgentId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LoanId)
                .ForeignKey("dbo.Agents", t => t.FK_ApprovalAgencyAgent)
                .ForeignKey("dbo.Agents", t => t.FK_LegalAgent)
                .ForeignKey("dbo.Agents", t => t.FK_PhysicalVerificationAgent)
                .ForeignKey("dbo.Agents", t => t.FK_PickupAgent)
                .ForeignKey("dbo.Users", t => t.FK_User)
                .ForeignKey("dbo.Agents", t => t.Agent_AgentId)
                .Index(t => t.FK_PhysicalVerificationAgent)
                .Index(t => t.FK_ApprovalAgencyAgent)
                .Index(t => t.FK_PickupAgent)
                .Index(t => t.FK_LegalAgent)
                .Index(t => t.FK_User)
                .Index(t => t.Agent_AgentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Answer1 = c.String(nullable: false),
                        Answer2 = c.String(nullable: false),
                        Answer3 = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        Issue = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateTicket = c.DateTime(nullable: false),
                        Resolution = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "Agent_AgentId", "dbo.Agents");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Loans", "FK_User", "dbo.Users");
            DropForeignKey("dbo.Loans", "FK_PickupAgent", "dbo.Agents");
            DropForeignKey("dbo.Loans", "FK_PhysicalVerificationAgent", "dbo.Agents");
            DropForeignKey("dbo.Loans", "FK_LegalAgent", "dbo.Agents");
            DropForeignKey("dbo.Loans", "FK_ApprovalAgencyAgent", "dbo.Agents");
            DropForeignKey("dbo.Agents", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Loans", new[] { "Agent_AgentId" });
            DropIndex("dbo.Loans", new[] { "FK_User" });
            DropIndex("dbo.Loans", new[] { "FK_LegalAgent" });
            DropIndex("dbo.Loans", new[] { "FK_PickupAgent" });
            DropIndex("dbo.Loans", new[] { "FK_ApprovalAgencyAgent" });
            DropIndex("dbo.Loans", new[] { "FK_PhysicalVerificationAgent" });
            DropIndex("dbo.Agents", new[] { "DepartmentId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Users");
            DropTable("dbo.Loans");
            DropTable("dbo.Departments");
            DropTable("dbo.Agents");
            DropTable("dbo.Admins");
        }
    }
}
