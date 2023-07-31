namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme_ : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        SenderMail = c.String(maxLength: 60),
                        ReceiverMail = c.String(maxLength: 60),
                        Subject = c.String(maxLength: 100),
                        MessageContent = c.String(),
                        MessageDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Headings", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Contents", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Contents", "HeadingID", "dbo.Headings");
            DropForeignKey("dbo.Headings", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Contents", new[] { "WriterID" });
            DropIndex("dbo.Contents", new[] { "HeadingID" });
            DropIndex("dbo.Headings", new[] { "WriterID" });
            DropIndex("dbo.Headings", new[] { "CategoryID" });
            DropTable("dbo.Messages");
            DropTable("dbo.ImageFiles");
            DropTable("dbo.Contacts");
            DropTable("dbo.Writers");
            DropTable("dbo.Contents");
            DropTable("dbo.Headings");
            DropTable("dbo.Categories");
            DropTable("dbo.Abouts");
        }
    }
}
