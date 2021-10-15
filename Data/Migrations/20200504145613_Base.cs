using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Data.Migrations
{
    public partial class Base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calle = table.Column<string>(nullable: true),
                    Altura = table.Column<int>(nullable: false),
                    Partido = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Licencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Licencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descipcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedioPago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedioPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsable_Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Correo_Electronico = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsable_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Equipo_Medicion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Equipo_Medicion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Licencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Cantidad_Maxima_Usuarios = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Licencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVisita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVisita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Familia",
                schema: "Seguridad",
                columns: table => new
                {
                    IdFamilia = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    Nombre = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Familia__751F80CFB2BE0BA2", x => x.IdFamilia);
                });

            migrationBuilder.CreateTable(
                name: "Patente",
                schema: "Seguridad",
                columns: table => new
                {
                    IdPatente = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    Nombre = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patente__9F4EF95C6B5370D7", x => x.IdPatente);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "Seguridad",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Contraseña = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF9736E01C24", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Responsable_Empresa_FK = table.Column<int>(nullable: false),
                    Direccion_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresa_Direccion_Direccion_FK",
                        column: x => x.Direccion_FK,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empresa_Responsable_Empresa_Responsable_Empresa_FK",
                        column: x => x.Responsable_Empresa_FK,
                        principalTable: "Responsable_Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Familia_Familia",
                schema: "Seguridad",
                columns: table => new
                {
                    IdFamilia = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    IdFamiliaHijo = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Familia___ABFCC67E4660EA48", x => new { x.IdFamilia, x.IdFamiliaHijo });
                    table.ForeignKey(
                        name: "FK__Familia_F__IdFam__300424B4",
                        column: x => x.IdFamilia,
                        principalSchema: "Seguridad",
                        principalTable: "Familia",
                        principalColumn: "IdFamilia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Familia_A__Famil__37A5467C",
                        column: x => x.IdFamiliaHijo,
                        principalSchema: "Seguridad",
                        principalTable: "Familia",
                        principalColumn: "IdFamilia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Familia_Patente",
                schema: "Seguridad",
                columns: table => new
                {
                    IdFamilia = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    IdPatente = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FamiliaE__166FEEA61367E606", x => new { x.IdFamilia, x.IdPatente });
                    table.ForeignKey(
                        name: "FK_Familia_Patente_Familia",
                        column: x => x.IdFamilia,
                        principalSchema: "Seguridad",
                        principalTable: "Familia",
                        principalColumn: "IdFamilia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamiliaElement_Patente",
                        column: x => x.IdPatente,
                        principalSchema: "Seguridad",
                        principalTable: "Patente",
                        principalColumn: "IdPatente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Familia",
                schema: "Seguridad",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    IdFamilia = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario___BC34479B236F871A", x => new { x.IdUsuario, x.IdFamilia });
                    table.ForeignKey(
                        name: "FK__Usuario_P__Famil__35BCFE0A",
                        column: x => x.IdFamilia,
                        principalSchema: "Seguridad",
                        principalTable: "Familia",
                        principalColumn: "IdFamilia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Usuario_F__IdUsu__33D4B598",
                        column: x => x.IdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Patente",
                schema: "Seguridad",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    IdPatente = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    timestamp = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Patente", x => new { x.IdUsuario, x.IdPatente });
                    table.ForeignKey(
                        name: "FK_Usuario_Patente_Patente",
                        column: x => x.IdPatente,
                        principalSchema: "Seguridad",
                        principalTable: "Patente",
                        principalColumn: "IdPatente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Patente_Usuario",
                        column: x => x.IdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Responsable_Empresa_FK = table.Column<int>(nullable: false),
                    Direccion_FK = table.Column<int>(nullable: false),
                    EmpresaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Direccion_Direccion_FK",
                        column: x => x.Direccion_FK,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cliente_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Responsable_Empresa_Responsable_Empresa_FK",
                        column: x => x.Responsable_Empresa_FK,
                        principalTable: "Responsable_Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Correo_Electronico = table.Column<string>(nullable: true),
                    Usuario_FK = table.Column<int>(nullable: false),
                    UsuarioIdUsuario = table.Column<string>(nullable: false),
                    Empresa_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Empresa_Empresa_FK",
                        column: x => x.Empresa_FK,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalSchema: "Seguridad",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipo_Medicion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Tipo_Equipo_Medicion_FK = table.Column<int>(nullable: false),
                    EmpresaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipo_Medicion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipo_Medicion_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipo_Medicion_Tipo_Equipo_Medicion_Tipo_Equipo_Medicion_FK",
                        column: x => x.Tipo_Equipo_Medicion_FK,
                        principalTable: "Tipo_Equipo_Medicion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empresa_FK = table.Column<int>(nullable: false),
                    Medio_Pago_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Empresa_Empresa_FK",
                        column: x => x.Empresa_FK,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pago_MedioPago_Medio_Pago_FK",
                        column: x => x.Medio_Pago_FK,
                        principalTable: "MedioPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Establecimiento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direccion_FK = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establecimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Establecimiento_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Establecimiento_Direccion_Direccion_FK",
                        column: x => x.Direccion_FK,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Plan_FK = table.Column<int>(nullable: false),
                    Cliente_FK = table.Column<int>(nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(nullable: true),
                    Empleado_Creador_FK = table.Column<int>(nullable: false),
                    Estado_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plan_Cliente_Cliente_FK",
                        column: x => x.Cliente_FK,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_Empleado_Empleado_Creador_FK",
                        column: x => x.Empleado_Creador_FK,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Plan_EstadoPlan_Estado_FK",
                        column: x => x.Estado_FK,
                        principalTable: "EstadoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_TipoPlan_Tipo_Plan_FK",
                        column: x => x.Tipo_Plan_FK,
                        principalTable: "TipoPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empresa_FK = table.Column<int>(nullable: false),
                    Tipo_Licencia_FK = table.Column<int>(nullable: false),
                    Fecha_Inicio = table.Column<DateTime>(nullable: true),
                    Fecha_Fin = table.Column<DateTime>(nullable: true),
                    Estado_FK = table.Column<int>(nullable: false),
                    Pago_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licencia_Empresa_Empresa_FK",
                        column: x => x.Empresa_FK,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licencia_Estado_Licencia_Estado_FK",
                        column: x => x.Estado_FK,
                        principalTable: "Estado_Licencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licencia_Pago_Pago_FK",
                        column: x => x.Pago_FK,
                        principalTable: "Pago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Licencia_Tipo_Licencia_Tipo_Licencia_FK",
                        column: x => x.Tipo_Licencia_FK,
                        principalTable: "Tipo_Licencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Location = table.Column<Point>(type: "geometry", nullable: true),
                    Establecimiento_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Establecimiento_Establecimiento_FK",
                        column: x => x.Establecimiento_FK,
                        principalTable: "Establecimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plan_Establecimiento",
                columns: table => new
                {
                    Establecimiento_FK = table.Column<int>(nullable: false),
                    Plan_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan_Establecimiento", x => new { x.Plan_FK, x.Establecimiento_FK });
                    table.ForeignKey(
                        name: "FK_Plan_Establecimiento_Establecimiento_Establecimiento_FK",
                        column: x => x.Establecimiento_FK,
                        principalTable: "Establecimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plan_Establecimiento_Plan_Plan_FK",
                        column: x => x.Plan_FK,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Visita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Establecimiento_FK = table.Column<int>(nullable: false),
                    Tipo_Visita_FK = table.Column<int>(nullable: false),
                    Empleado_FK = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: true),
                    Mes_Pactado = table.Column<int>(nullable: false),
                    Plan_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visita_Empleado_Empleado_FK",
                        column: x => x.Empleado_FK,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Establecimiento_Establecimiento_FK",
                        column: x => x.Establecimiento_FK,
                        principalTable: "Establecimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visita_Plan_Plan_FK",
                        column: x => x.Plan_FK,
                        principalTable: "Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visita_TipoVisita_Tipo_Visita_FK",
                        column: x => x.Tipo_Visita_FK,
                        principalTable: "TipoVisita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Direccion_FK",
                table: "Cliente",
                column: "Direccion_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EmpresaId",
                table: "Cliente",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Responsable_Empresa_FK",
                table: "Cliente",
                column: "Responsable_Empresa_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Empresa_FK",
                table: "Empleado",
                column: "Empresa_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_UsuarioIdUsuario",
                table: "Empleado",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Direccion_FK",
                table: "Empresa",
                column: "Direccion_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Responsable_Empresa_FK",
                table: "Empresa",
                column: "Responsable_Empresa_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_Medicion_EmpresaId",
                table: "Equipo_Medicion",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_Medicion_Tipo_Equipo_Medicion_FK",
                table: "Equipo_Medicion",
                column: "Tipo_Equipo_Medicion_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimiento_ClienteId",
                table: "Establecimiento",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimiento_Direccion_FK",
                table: "Establecimiento",
                column: "Direccion_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Licencia_Empresa_FK",
                table: "Licencia",
                column: "Empresa_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Licencia_Estado_FK",
                table: "Licencia",
                column: "Estado_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Licencia_Pago_FK",
                table: "Licencia",
                column: "Pago_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Licencia_Tipo_Licencia_FK",
                table: "Licencia",
                column: "Tipo_Licencia_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Empresa_FK",
                table: "Pago",
                column: "Empresa_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Medio_Pago_FK",
                table: "Pago",
                column: "Medio_Pago_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Cliente_FK",
                table: "Plan",
                column: "Cliente_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Empleado_Creador_FK",
                table: "Plan",
                column: "Empleado_Creador_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Estado_FK",
                table: "Plan",
                column: "Estado_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Tipo_Plan_FK",
                table: "Plan",
                column: "Tipo_Plan_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Establecimiento_Establecimiento_FK",
                table: "Plan_Establecimiento",
                column: "Establecimiento_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_Establecimiento_FK",
                table: "Ubicacion",
                column: "Establecimiento_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_Empleado_FK",
                table: "Visita",
                column: "Empleado_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_Establecimiento_FK",
                table: "Visita",
                column: "Establecimiento_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_Plan_FK",
                table: "Visita",
                column: "Plan_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_Tipo_Visita_FK",
                table: "Visita",
                column: "Tipo_Visita_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Familia_Familia_IdFamiliaHijo",
                schema: "Seguridad",
                table: "Familia_Familia",
                column: "IdFamiliaHijo");

            migrationBuilder.CreateIndex(
                name: "IX_Familia_Patente_IdPatente",
                schema: "Seguridad",
                table: "Familia_Patente",
                column: "IdPatente");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Familia_IdFamilia",
                schema: "Seguridad",
                table: "Usuario_Familia",
                column: "IdFamilia");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Patente_IdPatente",
                schema: "Seguridad",
                table: "Usuario_Patente",
                column: "IdPatente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipo_Medicion");

            migrationBuilder.DropTable(
                name: "Licencia");

            migrationBuilder.DropTable(
                name: "Plan_Establecimiento");

            migrationBuilder.DropTable(
                name: "Ubicacion");

            migrationBuilder.DropTable(
                name: "Visita");

            migrationBuilder.DropTable(
                name: "Familia_Familia",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Familia_Patente",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Usuario_Familia",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Usuario_Patente",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Tipo_Equipo_Medicion");

            migrationBuilder.DropTable(
                name: "Estado_Licencia");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Tipo_Licencia");

            migrationBuilder.DropTable(
                name: "Establecimiento");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "TipoVisita");

            migrationBuilder.DropTable(
                name: "Familia",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Patente",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "MedioPago");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "EstadoPlan");

            migrationBuilder.DropTable(
                name: "TipoPlan");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Responsable_Empresa");
        }
    }
}
