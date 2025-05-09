using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2PDS.Entities;

public partial class CursosContext : DbContext
{
    public CursosContext()
    {
    }

    public CursosContext(DbContextOptions<CursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<CursoImagen> CursoImagens { get; set; }

    public virtual DbSet<CursoParticipante> CursoParticipantes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Usuario_Accion> Usuario_Accions { get; set; }

    public virtual DbSet<Usuario_Sesion> Usuario_Sesions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=Cursos2025;uid=cursos;pwd=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Administrador", "Cursos2025");

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.rol).HasMaxLength(20);
            entity.Property(e => e.telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Curso", "Cursos2025");

            entity.HasIndex(e => e.idProfesor, "idProfesor");

            entity.Property(e => e.descripcion).HasMaxLength(255);
            entity.Property(e => e.fechaInicio).HasColumnType("datetime");
            entity.Property(e => e.fechaTermino).HasColumnType("datetime");
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.precio).HasPrecision(10);

            entity.HasOne(d => d.idProfesorNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.idProfesor)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("curso_ibfk_1");
        });

        modelBuilder.Entity<CursoImagen>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("CursoImagen", "Cursos2025");

            entity.HasIndex(e => e.idCurso, "idCurso");

            entity.Property(e => e.archivo).HasMaxLength(100);

            entity.HasOne(d => d.idCursoNavigation).WithMany(p => p.CursoImagens)
                .HasForeignKey(d => d.idCurso)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("cursoimagen_ibfk_1");
        });

        modelBuilder.Entity<CursoParticipante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("CursoParticipante", "Cursos2025");

            entity.HasIndex(e => e.idCurso, "idCurso");

            entity.HasIndex(e => e.idParticipante, "idParticipante");

            entity.Property(e => e.saldo).HasPrecision(10);

            entity.HasOne(d => d.idCursoNavigation).WithMany(p => p.CursoParticipantes)
                .HasForeignKey(d => d.idCurso)
                .HasConstraintName("cursoparticipante_ibfk_1");

            entity.HasOne(d => d.idParticipanteNavigation).WithMany(p => p.CursoParticipantes)
                .HasForeignKey(d => d.idParticipante)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("cursoparticipante_ibfk_2");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Pago", "Cursos2025");

            entity.HasIndex(e => e.idCurso, "idCurso");

            entity.HasIndex(e => e.idParticipante, "idParticipante");

            entity.Property(e => e.fechaPago).HasColumnType("datetime");
            entity.Property(e => e.monto).HasPrecision(10);

            entity.HasOne(d => d.idCursoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.idCurso)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pago_ibfk_2");

            entity.HasOne(d => d.idParticipanteNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.idParticipante)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("pago_ibfk_1");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Participante", "Cursos2025");

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("Profesor", "Cursos2025");

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.especializacion).HasMaxLength(200);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("Usuario", "Cursos2025");

            entity.Property(e => e.Activo).HasColumnType("bit(1)");
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.Materno).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Paterno).HasMaxLength(50);
            entity.Property(e => e.Puesto).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario_Accion>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("Usuario_Accion", "Cursos2025");

            entity.HasIndex(e => e.IDUsuario, "fk_usuario_accion_usuario");

            entity.HasIndex(e => e.IDUsuarioSesion, "fk_usuario_accion_usuario_sesion");

            entity.Property(e => e.Accion).HasMaxLength(500);
            entity.Property(e => e.FechaHora).HasColumnType("datetime");

            entity.HasOne(d => d.IDUsuarioNavigation).WithMany(p => p.Usuario_Accions)
                .HasForeignKey(d => d.IDUsuario)
                .HasConstraintName("fk_usuario_accion_usuario");

            entity.HasOne(d => d.IDUsuarioSesionNavigation).WithMany(p => p.Usuario_Accions)
                .HasForeignKey(d => d.IDUsuarioSesion)
                .HasConstraintName("fk_usuario_accion_usuario_sesion");
        });

        modelBuilder.Entity<Usuario_Sesion>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");

            entity.ToTable("Usuario_Sesion", "Cursos2025");

            entity.HasIndex(e => e.IDUsuario, "fk_UsuarioSesion_Usuario");

            entity.Property(e => e.DireccionIP).HasMaxLength(20);
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaUltimoAcceso).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(600);

            entity.HasOne(d => d.IDUsuarioNavigation).WithMany(p => p.Usuario_Sesions)
                .HasForeignKey(d => d.IDUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UsuarioSesion_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
