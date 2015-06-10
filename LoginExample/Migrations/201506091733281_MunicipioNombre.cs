namespace LoginExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MunicipioNombre : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Municipios", "Nombre", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Municipios", "Nombre", c => c.Int(nullable: false));
        }
    }
}
