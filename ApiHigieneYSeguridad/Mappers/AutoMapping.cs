using AutoMapper;
using Model;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities.Seguridad;
using Model.Seguridad;
using Data.Entities.EntidadesNoPersistidas;
using Model.PlanModels;
using Model.Visita;
using Model.Establecimiento;
using Model.Empresa;
using Model.Cliente;
using Model.TipoLicencia;

namespace ApiHigieneYSeguridad.Mappers
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            //Empresa
            CreateMap<EmpresaDTO, Empresa>()
               .ForMember(destinationMember: dest => dest.Responsable,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Responsable));            
            
            CreateMap<Empresa, EmpresaDTO>()
               .ForMember(destinationMember: dest => dest.Responsable,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Responsable));
            
            CreateMap<Empresa, EmpresaSummaryDTO>()
               .ForMember(destinationMember: dest => dest.Direccion,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Direccion.Calle +" "+ map.Direccion.Altura + ". " + 
                                                    map.Direccion.Partido.Nombre + ", " + map.Direccion.Partido.Provincia.Nombre)))
               .ForMember(destinationMember: dest => dest.ResponsableNombre,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Responsable.Apellido +", "+ map.Responsable.Nombre));

            CreateMap<ResponsableEmpresaDTO, ResponsableEmpresa>();
            CreateMap<DireccionDTO, Direccion>();
            CreateMap<Direccion, DireccionDTO>()
                .ForMember(destinationMember: dest => dest.PartidoNombre,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Partido.Nombre))
                .ForMember(destinationMember: dest => dest.ProvinciaId,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Partido.ProvinciaId))
               .ForMember(destinationMember: dest => dest.ProvinciaNombre,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Partido.Provincia.Nombre));

            //Empleado
            CreateMap<EmpleadoDTO, Empleado>()
               .ForMember(destinationMember: dest => dest.Usuario,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Usuario));
            
            CreateMap<Empleado, EmpleadoDTO>()
               .ForMember(destinationMember: dest => dest.Usuario,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Usuario));

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destinationMember: dest => dest.UsuarioFamilia,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.UsuarioRoles));
            
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destinationMember: dest => dest.UsuarioRoles,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.UsuarioFamilia));            

            CreateMap<Familia, FamiliaDTO>();
            CreateMap<FamiliaDTO, Familia>();
            CreateMap<UsuarioRolDTO, UsuarioFamilia>();
            CreateMap<UsuarioFamilia, UsuarioRolDTO> ();


            CreateMap<UsuarioDetalle, UsuarioDetalleDTO> ();
            CreateMap<UsuarioDetalleDTO, UsuarioDetalle> ();

            //Plan

            CreateMap<PlanesPorCliente, PlanesPorClienteDTO>()
                .ForMember(destinationMember: dest => dest.Plan,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Plan))
                .ForMember(destinationMember: dest => dest.Totalizados,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Totalizados));

            CreateMap<PlanesPorClienteDTO, PlanesPorCliente>()
                .ForMember(destinationMember: dest => dest.Plan,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Plan))
                .ForMember(destinationMember: dest => dest.Totalizados,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Totalizados));


            CreateMap<Plan, PlanClienteDTO>()
                .ForMember(destinationMember: dest => dest.EmpleadoNombre,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Empleado.Apellido + ", " + map.Empleado.Nombre )))
                .ForMember(destinationMember: dest => dest.Estado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Estado.Descipcion))
                .ForMember(destinationMember: dest => dest.TipoPlan,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.TipoPlan.Nombre));                
            
            CreateMap<Plan, PlanEmpresaDTO>()
                .ForMember(destinationMember: dest => dest.UsuarioCreador,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Empleado.Apellido + ", " + map.Empleado.Nombre )))
                .ForMember(destinationMember: dest => dest.Estado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Estado.Descipcion))
                .ForMember(destinationMember: dest => dest.FechaCreacion,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.FechaCreacion))
                .ForMember(destinationMember: dest => dest.Establecimientos,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.PlanesEstablecimientos.Count()))
                .ForMember(destinationMember: dest => dest.NombreCliente,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Cliente.Nombre))
                .ForMember(destinationMember: dest => dest.VisitasPendientes,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Visitas.Where(v => v.EstadoId.Equals(1) || v.EstadoId.Equals(2)).Count()))
                .ForMember(destinationMember: dest => dest.TipoPlan,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.TipoPlan.Nombre));            
            
            CreateMap<Plan, PlanDetalleDTO>()
                .ForMember(destinationMember: dest => dest.usuarioCreador,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Empleado.Apellido + ", " + map.Empleado.Nombre )))
                .ForMember(destinationMember: dest => dest.Cliente,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Cliente.Nombre)))
                .ForMember(destinationMember: dest => dest.IdCliente,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Cliente.Id)))
                .ForMember(destinationMember: dest => dest.Estado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Estado.Descipcion))
                .ForMember(destinationMember: dest => dest.Tipo,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.TipoPlan.Nombre))
                .ForMember(destinationMember: dest => dest.Visitas,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Visitas));

            CreateMap<Visita, VisitaSummaryDTO>()
                .ForMember(destinationMember: dest => dest.EmpleadoAsignado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Empleado.Apellido + ", " + map.Empleado.Nombre)))
                .ForMember(destinationMember: dest => dest.Estado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Estado.Descripcion)))
                .ForMember(destinationMember: dest => dest.Fecha,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Fecha)))
                .ForMember(destinationMember: dest => dest.NombreCliente,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Establecimiento.Cliente.Nombre)))
                .ForMember(destinationMember: dest => dest.NombreEstablecimiento,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Establecimiento.Nombre)))
                .ForMember(destinationMember: dest => dest.TipoVisita,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.TipoVisita.Descripcion)))
                .ForMember(destinationMember: dest => dest.FechaPactada,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (new DateTime(map.AnioPactado, map.MesPactado, 01))));

            CreateMap<Establecimiento, EstablecimientoSummaryDTO>()
                .ForMember(destinationMember: dest => dest.CantidadUbicaciones,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Ubicaciones.Count)));
                /*.ForMember(destinationMember: dest => dest.Nombre,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Nombre)))
                .ForMember(destinationMember: dest => dest.id,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Id)))
                .ForMember(destinationMember: dest => dest.Direccion,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Direccion)));*/

            CreateMap<Irregularidad, IrregularidadDTO>()
                .ForMember(destinationMember: dest => dest.Estado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Estado.Descripcion))) 
                .ForMember(destinationMember: dest => dest.Empleado,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Empleado.Apellido + ", " + map.Empleado.Nombre)))          
                .ForMember(destinationMember: dest => dest.Tipo,
                memberOptions: opt => opt.MapFrom(mapExpression: map => (map.Tipo.Descripcion)));
            CreateMap<IrregularidadDTO, Irregularidad>()
                .ForMember(destinationMember: dest => dest.Estado, 
                memberOptions: opt => opt.Ignore())
                .ForMember(destinationMember: dest => dest.Empleado,
                memberOptions: opt => opt.Ignore()) 
                .ForMember(destinationMember: dest => dest.Cliente,
                memberOptions: opt => opt.Ignore())
                .ForMember(destinationMember: dest => dest.Tipo,
                 memberOptions: opt => opt.Ignore());

            CreateMap<VisitaCambiarEstadoDTO, Visita>();                       
            CreateMap<Visita, VisitaCambiarEstadoDTO>();                    
            
            CreateMap<PlanDTO, Plan>();                       
            CreateMap<Plan, PlanDTO>();              
            
            CreateMap<PlanCambiarEstadoDTO, Plan>();                       
            CreateMap<Plan, PlanCambiarEstadoDTO>();            
            
            CreateMap<TokenDTO, Token>();                       
            CreateMap<Token, TokenDTO>();

            CreateMap<VisitaSummary, VisitaSummaryDTO>();                       
            CreateMap<VisitaSummaryDTO, VisitaSummary>();

            CreateMap<TotalizadoPlanes, TotalizadosPlanesDTO>();
            CreateMap<TotalizadosPlanesDTO, TotalizadoPlanes>();

            CreateMap<PlanEstablecimientoDTO, PlanEstablecimiento>();
            CreateMap<PlanEstablecimiento, PlanEstablecimientoDTO>();

            CreateMap<VisitaDTO, Visita>();
            CreateMap<Visita, VisitaDTO>();

            CreateMap<EquipoMedicionDTO, EquipoMedicion>();
            CreateMap<EquipoMedicionTotalizadoDTO, EquipoMedicionTotalizado>();
            CreateMap<EquipoMedicionTotalizado, EquipoMedicionTotalizadoDTO>();

            CreateMap<TipoPlanDTO, TipoPlan>();
            CreateMap<TipoPlan, TipoPlanDTO>(); 
            
            CreateMap<Establecimiento, EstablecimientoDTO>();
            CreateMap<EstablecimientoDTO, Establecimiento>();
            
            CreateMap<Ubicacion, UbicacionDTO>();
            CreateMap<UbicacionDTO, Ubicacion>();            
            
            CreateMap<Pago, PagoDTO>();
            CreateMap<PagoDTO, Pago>();            
            
            CreateMap<MedioPago, MedioPagoDTO>();
            CreateMap<MedioPagoDTO, MedioPago>();            
            
            CreateMap<Licencia, LicenciaDTO>();
            CreateMap<LicenciaDTO, Licencia>();

            CreateMap<TipoLicencia, TipoLicenciaDTO>()
                .ForMember(destinationMember: dest => dest.PrecioActual,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.PreciosHistoricos.OrderByDescending(p => p.FechaDesde).FirstOrDefault().Precio))
                .ForMember(destinationMember: dest => dest.PreciosHistoricos,
                memberOptions: opt => opt.Ignore());
            CreateMap<TipoLicenciaDTO, TipoLicencia>();

            CreateMap<TipoLicencia, TipoLicenciaAltaDTO>();
            CreateMap<TipoLicenciaAltaDTO, TipoLicencia>();



            CreateMap<EstadoLicencia, EstadoLicenciaDTO>();
            CreateMap<EstadoLicenciaDTO, EstadoLicencia>();
            
            CreateMap<PrecioLicencia, PrecioLicenciaDTO>();
            CreateMap<PrecioLicenciaDTO, PrecioLicencia>();

            CreateMap<ClienteDTO, Cliente>()
                .ForMember(destinationMember: dest => dest.Establecimientos,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Establecimientos))
                .ForMember(destinationMember: dest => dest.Planes,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Planes));            
            
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(destinationMember: dest => dest.Establecimientos,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Establecimientos))
                .ForMember(destinationMember: dest => dest.Planes,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Planes));
            
            CreateMap<Cliente, ClienteSummaryDTO>()
                .ForMember(destinationMember: dest => dest.CantidadEstablecimientos,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Establecimientos.Count))
                .ForMember(destinationMember: dest => dest.CantidadPlanes,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Planes.Count))
            .ForMember(destinationMember: dest => dest.Id,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Id))
            .ForMember(destinationMember: dest => dest.Nombre,
                memberOptions: opt => opt.MapFrom(mapExpression: map => map.Nombre));



        }
    }
}
