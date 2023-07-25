namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update0330 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Benhs", "NgayBatDauChuaTri", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Benhs", "NgayBatDauChuaTri", c => c.DateTime(nullable: false));
        }
    }
}
