using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class spsSeguridad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"create procedure [Seguridad].[ObtenerPatentesUsuario]
			@Usuario varchar(50)
		as

		--Tabla temporal de almacenamiento de Patentes
		declare @Patentes Table (idPatente varchar(36),nombre varchar(1000))

				--Patentes Asociadas Directamente al usuario
				insert into @Patentes	 Select Patente.idPatente,Patente.Nombre from Seguridad.Usuario
											inner join Seguridad.Usuario_Patente on Usuario.idUsuario = Usuario_Patente.idUsuario
											inner join Seguridad.Patente on Patente.idPatente = Usuario_Patente.idPatente
											Where Usuario.idUsuario = @Usuario
		--Obtengo Familias de Usuario

		declare @Familias Table (ID varchar(36))

				insert into @Familias exec [Seguridad].[ObtenerFamiliasUsuario] @Usuario = @Usuario

		-- Obtengo Patentes de Familias

				DECLARE @IdFamilia varchar(36)

				DECLARE Grupos CURSOR 
				LOCAL STATIC READ_ONLY FORWARD_ONLY
				FOR 

					select * from @Familias

				OPEN Grupos
				FETCH NEXT FROM Grupos INTO @IdFamilia
				WHILE @@FETCH_STATUS = 0
				BEGIN 

					insert into @Patentes exec Seguridad.ObtenerPatentesFamilia @Familia = @IdFamilia
		
					FETCH NEXT FROM Grupos INTO @IdFamilia
				END
				CLOSE Grupos
				DEALLOCATE Grupos

			--Devuelvo lista patentes
			Select distinct * from @Patentes";

			var sp2 = @"CREATE procedure [Seguridad].[ObtenerPatentesFamilia]
						@Familia varchar(36)
					as

						select Patente.idPatente ,Patente.Nombre from Seguridad.Familia
							inner join Seguridad.Familia_Patente on Familia.idFamilia = Familia_Patente.idFamilia
							inner Join Seguridad.Patente on Patente.idPatente = familia_Patente.idPatente
							where Seguridad.Familia.idFamilia = @Familia";

			var sp3 = @"CREATE procedure [Seguridad].[ObtenerFamiliasUsuario]
					@Usuario varchar(50)
				as

				declare @Familias Table (ID varchar(36))
	
					DECLARE @IdFamiliaDirecta varchar(36)

					DECLARE GruposDirectos CURSOR 
					LOCAL STATIC READ_ONLY FORWARD_ONLY
					FOR 
			
						Select Familia.idFamilia from Seguridad.Usuario 
							inner join Seguridad.Usuario_Familia on Usuario.idUsuario = Usuario_Familia.idUsuario
							inner join Seguridad.Familia on Familia.idFamilia = Usuario_Familia.idFamilia
							where Seguridad.Usuario.idUsuario = @Usuario

					OPEN GruposDirectos
					FETCH NEXT FROM GruposDirectos INTO @IdFamiliaDirecta
					WHILE @@FETCH_STATUS = 0
					BEGIN 
		
						insert into @Familias(ID) values (@IdFamiliaDirecta) 

						;WITH FamiliasHijas (IdFamilia,IdFamiliaHijo)AS
						(
							Select Familia_Familia.idFamilia,Familia_Familia.idFamiliaHijo
							from Seguridad.Familia_Familia 
							where IdFamilia = @IdFamiliaDirecta 
   
						   UNION ALL
	
							Select Familia_Familia.idFamilia,Familia_Familia.idFamiliaHijo
							from Seguridad.Familia_Familia
							join FamiliasHijas on  Familia_Familia.IdFamilia = FamiliasHijas.IdFamiliaHijo
						)

						insert into @Familias Select IdFamiliaHijo FROM FamiliasHijas;

						FETCH NEXT FROM GruposDirectos INTO @IdFamiliaDirecta
					END
					CLOSE GruposDirectos
					DEALLOCATE GruposDirectos

					select * from @Familias";

			var sp4 = @"CREATE PROCEDURE [Seguridad].[ObtenerFamiliasHijasDeFamilia]   
							@Familia int  
						AS   

							WITH FamiliasHijas (IdFamilia,IdFamiliaHijo)
							AS
							(
							Select Familia_Familia.idFamilia,Familia_Familia.idFamiliaHijo
							from Seguridad.Familia_Familia 
							where IdFamilia = @familia 
   
						   UNION ALL
	
							Select Familia_Familia.idFamilia,Familia_Familia.idFamiliaHijo
							from Seguridad.Familia_Familia
							join FamiliasHijas on  Familia_Familia.IdFamilia = FamiliasHijas.IdFamiliaHijo
							)

						SELECT * FROM FamiliasHijas;";


			migrationBuilder.Sql(sp);
			migrationBuilder.Sql(sp2);
			migrationBuilder.Sql(sp3);
			migrationBuilder.Sql(sp4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
