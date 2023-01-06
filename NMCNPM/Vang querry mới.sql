create -- alter 
proc qtv_danhsachhslop @Nam varchar(12),@TenLop varchar(10)
as
begin try
	select * from HocSinh where MaHS in (select MaHocSinh from DanhSachLopHoc where TenLop = @TenLop and @Nam = Nam)
	select 0
end try
begin catch
	select 10
end catch
go

create -- alter
proc qtv_themHSvaoLop @Nam varchar(12),@TenLop varchar(10), @mahs int
as
begin tran
	begin try
		if not exists (select * from Lop where TenLop = @TenLop and Nam = @Nam)
		begin
			rollback tran
			select 1
			return
		end
		if 0 < (select dbo.qtv_CheckThemHSvaoLop(@Nam,@TenLop,@mahs))
		begin
			rollback tran
			select 2
			return
		end
		insert into DanhSachLopHoc values (@TenLop,@Nam,@mahs)
		select 0
	end try
	begin catch
		select 10
	end catch
commit tran
go

create -- alter
proc qtv_xoaHSkhoiLop @Nam varchar(12),@TenLop varchar(10), @mahs int
as
begin tran
	begin try
		if not exists (select * from Lop where TenLop = @TenLop and Nam = @Nam)
		begin
			rollback tran
			select 1
			return
		end
		delete DanhSachLopHoc where TenLop =@TenLop and Nam = @Nam and MaHocSinh = @mahs
		select 0
	end try
	begin catch
		select 10
	end catch
commit tran
go
create -- alter 
function qtv_CheckThemHSvaoLop (@Nam varchar(12),@TenLop varchar(10), @mahs int)
returns tinyint
as
begin 
	declare @LopHienTai int
	if exists (select * from DanhSachLopHoc where MaHocSinh = @mahs and Nam = @Nam)
		return 1
	set @LopHienTai =  (select max(SUBSTRING(TenLop, 1, 2)) from DanhSachLopHoc where MaHocSinh = @mahs)
	--if (@LopHienTai is null and (SUBSTRING(@tenlop, 1, 2) = 10))
	--	return 1
	--if @LopHienTai + 1 = (SUBSTRING(@tenlop, 1, 2))
	--	return 1
	if @LopHienTai < (SUBSTRING(@tenlop, 1, 2))
		return 1
	return 0
end 
go


create -- alter 
proc qtv_tracuudanhsachlop @keyword nvarchar(20),@Nam varchar(12),@TenLop varchar(10)
as
begin try
	select *
	from HocSinh
	where
		(HoTen like '%'+ @keyword +'%' or
		NgaySinh like '%'+  @keyword +'%' or
		GioiTinh like '%'+  @keyword +'%' or
		Email like '%'+  @keyword +'%' or
		SDT like '%'+  @keyword +'%' or
		DiaChi like '%'+  @keyword +'%' or
		username like '%'+  @keyword+'%') and
		MaHS in (select MaHocSinh from DanhSachLopHoc  where Nam like '%'+  @Nam+'%' and TenLop like '%'+  @TenLop+'%')
end try
begin catch
end catch
go

create -- alter 
proc qtv_tracuu @keyword nvarchar(20)
as
begin try
	select *
	from (select MaHS,HoTen,NgaySinh,GioiTinh,Email,SDT,DiaChi,null MonDay,username,N'Học sinh' as 'ChucDanh' from dbo.HocSinh
		UNION
		select MaQT,HoTen,NgaySinh,GioiTinh,Email,SDT,DiaChi,null MonDay,username,N'Quản trị' as 'ChucDanh' from dbo.QuanTri
		UNION
		select *,N'Giáo viên' as 'Role' from dbo.GiaoVien) a
	where
		HoTen like '%'+ @keyword +'%' or
		NgaySinh like '%'+  @keyword +'%' or
		GioiTinh like '%'+  @keyword +'%' or
		Email like '%'+  @keyword +'%' or
		SDT like '%'+  @keyword +'%' or
		DiaChi like '%'+  @keyword +'%' or
		MonDay like '%'+  @keyword +'%' or
		username like '%'+  @keyword+'%'  or
		ChucDanh like '%'+  @keyword +'%'
end try
begin catch
end catch
go


create -- alter 
proc qtv_danhsachtaikhoan @keyword nvarchar(20),@role tinyint
as
begin try
	select *
	from (select MaHS,HoTen,NgaySinh,GioiTinh,Email,SDT,DiaChi,null MonDay,username,3 as 'ChucDanh' from dbo.HocSinh
		UNION
		select MaQT,HoTen,NgaySinh,GioiTinh,Email,SDT,DiaChi,null MonDay,username,1 from dbo.QuanTri
		UNION
		select *,2  from dbo.GiaoVien) a
	where
		(HoTen like '%'+ @keyword +'%' or
		NgaySinh like '%'+  @keyword +'%' or
		GioiTinh like '%'+  @keyword +'%' or
		Email like '%'+  @keyword +'%' or
		SDT like '%'+  @keyword +'%' or
		DiaChi like '%'+  @keyword +'%' or
		MonDay like '%'+  @keyword +'%' or
		username like '%'+  @keyword+'%'  or
		ChucDanh like '%'+  @keyword +'%') and
		ChucDanh = @role
end try
begin catch
end catch
go


create -- alter 
proc qtv_themtaikhoan
	@role int,
	@username varchar(20),
	@pass varchar(50),
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50),
	@SDT bigint,
	@DiaChi nvarchar(50),
	@MonDay nvarchar(20)
as
begin tran

begin try
	if @username = '' or 
		@pass = '' or 
		@HoTen = '' or 
		@NgaySinh = '' or 
		@GioiTinh = '' or 
		@Email = '' or 
		@SDT = '' or 
		@DiaChi = '' 
	begin
		rollback tran
		select -1
		return 
	end

	if exists (select * from TaiKhoan where username = @username)
	begin
		rollback tran
		select -2
		return 
	end
	insert into TaiKhoan values (@username, @pass, @role)
	declare @ma int
	if (@role = 1)
	begin
		set @ma = (select max(MaQT) from QuanTri)
		if @ma is null
			set @ma = 1
		else set @ma = @ma +1
		insert into QuanTri values (@ma,@HoTen,@NgaySinh,@GioiTinh,@Email,@SDT,@DiaChi,@username)
	end
	else if (@role = 2)
	begin
		set @ma = (select max(MaGV) from GiaoVien)
		if @ma is null
			set @ma = 1
		else set @ma = @ma +1
		insert into GiaoVien values (@ma,@HoTen,@NgaySinh,@GioiTinh,@Email,@SDT,@DiaChi,@MonDay,@username)
	end
	else if (@role = 3)
	begin
		set @ma = (select max(MaHS) from HocSinh)
		if @ma is null
			set @ma = 1
		else set @ma = @ma +1
		insert into HocSinh values (@ma,@HoTen,@NgaySinh,@GioiTinh,@Email,@SDT,@DiaChi,@username)
	end
	select @ma
end try
begin catch
	rollback tran
	select -10
	return
end catch
commit tran
go
create -- alter 
proc qtv_suataikhoan
	@role int,
	@username varchar(20),
	@pass varchar(50),
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50),
	@SDT bigint,
	@DiaChi nvarchar(50),
	@MonDay nvarchar(20)
as
begin tran

begin try
	if @username = '' or 
		@HoTen = '' or 
		@NgaySinh = '' or 
		@GioiTinh = '' or 
		@Email = '' or 
		@SDT = '' or 
		@DiaChi = '' 
	begin
		rollback tran
		select -1
		return 
	end

	if not exists (select * from TaiKhoan where username = @username)
	begin
		rollback tran
		select -3
		return 
	end
	if @pass != ''
		update TaiKhoan set pass = @pass where username = @username
	declare @ma int
	if (@role = 1)
	begin
		update QuanTri set HoTen=@HoTen,NgaySinh = @NgaySinh,GioiTinh=@GioiTinh,Email=@Email,
			SDT=@SDT,DiaChi=@DiaChi where username = @username
		set @ma = (select MaQT from QuanTri where username = @username)
	end
	else if (@role = 2)
	begin
		update GiaoVien set HoTen=@HoTen,NgaySinh = @NgaySinh,GioiTinh=@GioiTinh,Email=@Email,
			SDT=@SDT,DiaChi=@DiaChi, Monday =@MonDay where username = @username
		set @ma = (select MaGV from GiaoVien where username = @username)
	end
	else if (@role = 3)
	begin
		update HocSinh set HoTen=@HoTen,NgaySinh = @NgaySinh,GioiTinh=@GioiTinh,Email=@Email,
			SDT=@SDT,DiaChi=@DiaChi where username = @username
		set @ma = (select MaHS from HocSinh where username = @username)
	end
	select @ma
end try
begin catch
	rollback tran
	select -10
	return
end catch
commit tran
go

create proc qtv_ThemGiaoVienLopHoc
	@MaGV int,
	@TenLop varchar(10),
	@Nam varchar(12)
as
begin tran
	begin try
		if @MaGV = '' or @TenLop = '' or @Nam= ''
		begin
			rollback tran
			select 1
			return 
		end
		if not exists (select * from Lop where TenLop = @TenLop and Nam = @Nam)
		begin
			rollback tran
			select 2
			return 
		end
		insert into GiaoVien_LopHoc values (@MaGV,@TenLop,@Nam)
		select 0
	end try
	begin catch
		rollback tran
		select 10
		return 
	end catch
commit tran
go

create --alter 
proc qtv_dslopgvdangday @magv int
as
begin
	select *,(select 'X' from GiaoVien_LopHoc where l.TenLop = TenLop and l.Nam = Nam and MaGV = @magv)TinhTrang from Lop l
end
go

create -- alter
proc qtv_xoataikhoan
	@username varchar(20)
as
begin tran
	begin try
		if @username = ''
		begin
			rollback tran
			select 1
			return 
		end

		declare @role int
		set @role = (select Loai from TaiKhoan where username = @username)

		declare @ma int
		if (@role = 1)
		begin
			set @ma = (select MaQT from QuanTri where username = @username)
			delete QuanTri where username = @username
		end
		else if (@role = 2)
		begin
			set @ma = (select MaGV from GiaoVien where username = @username)
			if  exists (select * from GiaoVien_LopHoc where MaGV = @ma)
			begin
				rollback tran
				select 2
				return 
			end
			delete GiaoVien where username = @username
		end
		else if (@role = 3)
		begin
			set @ma = (select MaHS from HocSinh where username = @username)
			if  exists (select * from DanhSachLopHoc where MaHocSinh = @ma)
			begin
				rollback tran
				select 2
				return 
			end
			delete HocSinh where username = @username
		end
		delete TaiKhoan where username = @username
		select 0
	end try
	begin catch
		rollback tran
		select 10
		return 
	end catch
commit tran
go
qtv_xoataikhoan 'quantrithemlan3'