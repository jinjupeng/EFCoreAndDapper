using ApiServer.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model.Contexts
{
    public partial class ContextMySql : DbContext
    {
        public ContextMySql()
        {
        }

        public ContextMySql(DbContextOptions<ContextMySql> options)
            : base(options)
        {
        }

        public virtual DbSet<sys_org> sys_org { get; set; }
        public virtual DbSet<sys_user> sys_user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=mpshop;user id=root;password=123456;allow user variables=True", x => x.ServerVersion("8.0.13-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<sys_org>(entity =>
            {
                entity.HasComment("系统组织结构表");

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.address)
                    .HasColumnType("varchar(64)")
                    .HasComment("地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.email)
                    .HasColumnType("varchar(32)")
                    .HasComment("邮件")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.is_leaf).HasComment("0:不是叶子节点，1:是叶子节点");

                entity.Property(e => e.level)
                    .HasColumnType("int(11)")
                    .HasComment("组织层级");

                entity.Property(e => e.org_name)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasComment("组织名")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.org_pid)
                    .HasColumnType("int(11)")
                    .HasComment("上级组织编码");

                entity.Property(e => e.org_pids)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasComment("所有的父节点id")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.phone)
                    .HasColumnType("varchar(13)")
                    .HasComment("电话")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.sort)
                    .HasColumnType("int(11)")
                    .HasComment("排序");

                entity.Property(e => e.status).HasComment("是否禁用，0:启用(否）,1:禁用(是)");
            });

            modelBuilder.Entity<sys_user>(entity =>
            {
                entity.HasComment("用户信息表");

                entity.HasIndex(e => e.username)
                    .HasName("username")
                    .IsUnique();

                entity.Property(e => e.id).HasColumnType("int(11)");

                entity.Property(e => e.create_time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("用户创建时间")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.email)
                    .HasColumnType("varchar(32)")
                    .HasComment("email")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.enabled)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'")
                    .HasComment("0无效用户，1是有效用户");

                entity.Property(e => e.nickname)
                    .HasColumnType("varchar(64)")
                    .HasComment("昵称")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.org_id)
                    .HasColumnType("int(11)")
                    .HasComment("组织id");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("密码")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.phone)
                    .HasColumnType("varchar(16)")
                    .HasComment("手机号")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.portrait)
                    .HasColumnType("varchar(255)")
                    .HasComment("头像图片路径")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("用户名")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}