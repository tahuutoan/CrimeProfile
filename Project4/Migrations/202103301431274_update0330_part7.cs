namespace Project4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update0330_part7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ThamGuis", "NgayThamGui", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ThamGuis", "NgayThamGui", c => c.DateTime(nullable: false));
        }
    }
}
