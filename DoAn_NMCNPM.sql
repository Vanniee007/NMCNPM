create database NMCNPM
go
use NMCNPM
go
create table TaiKhoan
(
	username varchar(20), 
	pass varchar(50) not null,
	Loai tinyint not null,
	Constraint PK_TaiKhoan Primary key(username)
)
create table QuanTri
(
	MaQT int, 
	HoTen nvarchar(50),
	NgaySinh date,
	GioiTinh nvarchar(10),
	Email varchar(50), 
	SDT bigint, 
	DiaChi nvarchar(50),
	username varchar(20)
	Constraint PK_Doitac Primary key(MaQT),
	Foreign key(username) references TaiKhoan(username),
)

create table NamHoc
(
	Nam varchar(12),
	TuoiToiThieu int,
	TuoiToiDa int,
	Constraint PK_NamHoc Primary key(Nam)

)

create table MonHoc
(
	TenMon nvarchar(20),
	Nam varchar(12),
	DiemDat float
	Constraint PK_MonHoc Primary key(TenMon,Nam)
	Foreign key(Nam) references NamHoc(Nam)

)

create table Lop
(
	TenLop varchar(10),
	Nam varchar(12),
	SiSo int,
	Constraint PK_Lop Primary key(TenLop,Nam),
	Foreign key(Nam) references NamHoc(Nam)
)

create table HocSinh
(
	MaHS int, 
	HoTen nvarchar(50),
	NgaySinh date,
	GioiTinh nvarchar(10),
	Email varchar(50), 
	SDT bigint, 
	DiaChi nvarchar(50),
	username varchar(20)
	Constraint PK_HocSinh Primary key(MaHS),
	Foreign key(username) references TaiKhoan(username),
)
create table GiaoVien
(
	MaGV int, 
	HoTen nvarchar(50),
	NgaySinh date,
	GioiTinh nvarchar(10),
	Email varchar(50), 
	SDT bigint, 
	DiaChi nvarchar(50),
	MonDay nvarchar(20),
	username varchar(20)
	Constraint PK_GiaoVien Primary key(MaGV),
	Foreign key(username) references TaiKhoan(username),
	
)
create table  DanhSachLopHoc
(
	TenLop varchar(10),
	Nam varchar(12),
	MaHocSinh int
	Constraint PK_DanhSachLopHoc Primary key(TenLop,Nam,MaHocSinh),
	Foreign key(TenLop,Nam) references Lop(TenLop,Nam),
	Foreign key(MaHocSinh) references HocSinh(MaHS)
)
create table Diem_HocSinh_MonHoc
(
	MaHocSinh int,
	TenMon nvarchar(20),
	Nam varchar(12),
	KiHoc int,
	Diem15 float,
	Diem1Tiet float,
	DiemCuoiKi float
	Constraint PK_Diem_HocSinh_MonHoc Primary key(MaHocSinh,TenMon,Nam,KiHoc),
	Foreign key(TenMon,Nam) references MonHoc(TenMon,Nam),
	Foreign key(MaHocSinh) references HocSinh(MaHS)
)
create table GiaoVien_LopHoc
(
	MaGV int,
	TenLop varchar(10),
	Nam varchar(12),
	Constraint PK_GiaoVien_Sinh Primary Key(MaGV,TenLop,Nam),
	Foreign key(MaGV) references GiaoVien(MaGV),
	Foreign key(TenLop,Nam) references Lop(TenLop,Nam),
)
----------------------------------------
insert into TaiKhoan values('admin02','123',1)
insert into TaiKhoan values('admin01','123',1)
insert into TaiKhoan values('admin','123',1)

insert into TaiKhoan values('gv01','123',2)
insert into TaiKhoan values('gv02','123',2)
insert into TaiKhoan values('gv03','123',2)
insert into TaiKhoan values('gv04','123',2)




insert into TaiKhoan values('hs01','123',3)
insert into TaiKhoan values('hs02','123',3)
insert into TaiKhoan values('hs03','123',3)
insert into TaiKhoan values('hs04','123',3)
insert into TaiKhoan values('hs05','123',3)



insert into GiaoVien values(1,N'Anh','1-1-1978',N'Nam','abc@gmail.com','0123856789',N'Phus Tho',N'Toán','gv01')
insert into GiaoVien values(2,N'Anh Tú','1-1-1978',N'Nam','abd@gmail.com','0123756789',N'Phus Tho',N'Toán','gv02')
insert into GiaoVien values(3,N'Anh Phạm','1-1-1978',N'Nam','abe@gmail.com','0123454789',N'Phus Tho',N'Văn','gv03')
insert into GiaoVien values(4,N'Bao Thanh Quy','1-1-1978',N'Nam','baothanhquy@gmail.com','0123477789',N'Bình Dương',N'Văn','gv04')


insert into QuanTri values(1,N'Anh Phạm','1-1-1978',N'Nam','abw@gmail.com','0123454789',N'Phus Tho','admin')
insert into QuanTri values(2,N'Anh Phạm','1-1-1978',N'Nam','abq@gmail.com','0123454789',N'Phus Tho','admin01')
insert into QuanTri values(3,N'Anh Phạm','1-1-1978',N'Nam','adc@gmail.com','0123454789',N'Phus Tho','admin02')

insert into HocSinh values(1,N'Anh Phạm Quy','1-1-2000',N'Nam','hs01@gmail.com','0123454789',N'Phus Tho','hs01')
insert into HocSinh values(2,N'Mai Quyết ','1-1-2000',N'Nam','hs02@gmail.com','0123454789',N'Bình Dương','hs02')
insert into HocSinh values(3,N'Trương Anh Ngọc','1-1-2000',N'Nam','hs03@gmail.com','0123454789',N'Bình Dương','hs03')
insert into HocSinh values(4,N'Lê Thị Vy','1-1-2000',N'Nữ','hs04@gmail.com','0123454789',N'Bình Dương','hs04')
insert into HocSinh values(5,N'Lee Min Ho','1-1-2000',N'Nam','hs05@gmail.com','0369852741',N'Bình Dương','hs05')

insert into NamHoc values('2021-2022',16,20)
insert into NamHoc values('2020-2021',16,20)
insert into NamHoc values('2019-2020',16,20)

insert into MonHoc values(N'Toán','2019-2020',5.0)
insert into MonHoc values(N'Văn','2019-2020',5.0)
insert into MonHoc values(N'Sinh','2019-2020',5.0)

insert into Lop values('10A1','2019-2020',40)
insert into Lop values('10A2','2019-2020',40)
insert into Lop values('11A2','2019-2020',40)
insert into Lop values('11A1','2019-2020',40)


insert into Lop values('10A1','2021-2022',40)
insert into Lop values('10A1','2020-2021',40)
insert into Lop values('11A1','2021-2022',40)
insert into Lop values('11A2','2021-2022',40)

insert into DanhSachLopHoc values('10A1','2019-2020',1)
insert into DanhSachLopHoc values('10A1','2019-2020',2)
insert into DanhSachLopHoc values('10A1','2019-2020',3)
insert into DanhSachLopHoc values('10A1','2019-2020',4)


insert into Diem_HocSinh_MonHoc values(1,N'Toán','2019-2020',1,9,9,10)
insert into Diem_HocSinh_MonHoc values(1,N'Toán','2019-2020',2,9,9,10)
insert into Diem_HocSinh_MonHoc values(1,N'Văn','2019-2020',1,9,8,8)
insert into Diem_HocSinh_MonHoc values(1,N'Văn','2019-2020',2,9,9,7)
insert into Diem_HocSinh_MonHoc values(1,N'Sinh','2019-2020',1,1,9,10)
insert into Diem_HocSinh_MonHoc values(1,N'Sinh','2019-2020',2,7.5,9,10)

insert into Diem_HocSinh_MonHoc values(2,N'Toán','2019-2020',1,9,9,10)
insert into Diem_HocSinh_MonHoc values(2,N'Toán','2019-2020',2,9,9,10)
insert into Diem_HocSinh_MonHoc values(2,N'Văn','2019-2020',1,9,8,8)
insert into Diem_HocSinh_MonHoc values(2,N'Văn','2019-2020',2,9,9,7)
insert into Diem_HocSinh_MonHoc values(2,N'Sinh','2019-2020',1,1,3,5)
insert into Diem_HocSinh_MonHoc values(2,N'Sinh','2019-2020',2,4.99,4.9,4.8)

insert into Diem_HocSinh_MonHoc values(3,N'Toán','2019-2020',1,9,9,10)
insert into Diem_HocSinh_MonHoc values(3,N'Toán','2019-2020',2,9,9,10)
insert into Diem_HocSinh_MonHoc values(3,N'Văn','2019-2020',1,1.5,1,4)
insert into Diem_HocSinh_MonHoc values(3,N'Văn','2019-2020',2,3,5,4)
insert into Diem_HocSinh_MonHoc values(3,N'Sinh','2019-2020',1,1,9,6)
insert into Diem_HocSinh_MonHoc values(3,N'Sinh','2019-2020',2,7.5,9,5)

insert into Diem_HocSinh_MonHoc values(4,N'Toán','2019-2020',1,3,5,6)
insert into Diem_HocSinh_MonHoc values(4,N'Toán','2019-2020',2,7,4,6)
insert into Diem_HocSinh_MonHoc values(4,N'Văn','2019-2020',1,10,10,10)
insert into Diem_HocSinh_MonHoc values(4,N'Văn','2019-2020',2,9,9,9.5)
insert into Diem_HocSinh_MonHoc values(4,N'Sinh','2019-2020',1,1,9,10)
insert into Diem_HocSinh_MonHoc values(4,N'Sinh','2019-2020',2,7.5,9,10)


/*

delete from Diem_HocSinh_MonHoc 
go
delete  from DanhSachLopHoc
delete from Lop
delete from MonHoc*/

go

---------------------------QUẢN TRỊ VIÊN---------------------------
---------------------------------------------------------------------------------
---------------------------------------------------------------------------------
create -- alter 
proc qtv_danhsachhslop @Nam varchar(12),@TenLop varchar(10)
as
begin try
	select MaHS,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,username from HocSinh where MaHS in (select MaHocSinh from DanhSachLopHoc where TenLop = @TenLop and @Nam = Nam)
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
	select MaHS,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,username
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
	from (select MaHS,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,null MonDay,username,N'Học sinh' as 'ChucDanh' from dbo.HocSinh
		UNION
		select MaQT,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,null MonDay,username,N'Quản trị' as 'ChucDanh' from dbo.QuanTri
		UNION
		select MaGV,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi, MonDay,username,N'Giáo viên' as 'Role' from dbo.GiaoVien) a
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

create function SDT_To_Char(@sdt bigint)
returns varchar(10)
as
begin
	return '0'+cast(@sdt as varchar(9))
end
go
create -- alter 
proc qtv_danhsachtaikhoan @keyword nvarchar(20),@role tinyint
as
begin try
	select *
	from (select MaHS,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,null MonDay,username,3 as 'ChucDanh' from dbo.HocSinh
		UNION
		select MaQT,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,null MonDay,username,1 from dbo.QuanTri
		UNION
		select MaGV,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi,null MonDay,username,2  from dbo.GiaoVien) a
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

---------------------------THÔNG TIN
create --alter
proc QTV_LayThongTinTaiKhoan
	@username varchar(20)
as
begin tran
	begin try
		if @username=''
		begin 
			print N'Thông tin trống'
			rollback tran
			select -1
			return
		end
		if not exists(select* from TaiKhoan where @username=username)
		begin
			print N'Username không tồn tại'
			rollback tran
			select -2
			return
		end
		select *
		from TaiKhoan
		where @username=username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO

create proc QTV_LayThongTin
@username varchar(20)
as
begin 
	select* from QuanTri where username=@username
end
go

	


create --alter
proc QTV_CapNhatThongTinCaNhan
	@MaQT int,
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50), 
	@SDT bigint, 
	@DiaChi nvarchar(50)
as
begin tran
	begin try
		if @HoTen='' or  @NgaySinh='' or @GioiTinh='' or @Email='' or @SDT='' or @DiaChi=''
		begin
			print N'Có trường thông tin trống'
			rollback tran
			select -1
			return
		end
		update QuanTri
		set HoTen=@HoTen,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,Email=@Email,SDT=@SDT,DiaChi=@DiaChi
		where MaQT=@MaQT
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

--DoiMatKhau
create --alter
proc DoiMatKhau
	@username varchar(20),
	@matkhaucu varchar(50),
	@matkhaumoi varchar(50),
	@matkhaumoi2 varchar(50)
as
begin tran
	begin try
		if @matkhaucu!=(select pass from TaiKhoan where @username=username)
		begin
			print N'Mật khẩu cũ không chính xác'
			rollback tran
			select -1
			return
		end
		if @matkhaumoi=''
		begin
			print N'Mật khẩu mới rỗng'
			rollback tran
			select -2
			return
		end
		if @matkhaumoi!=@matkhaumoi2
		begin
			print N'Mật khẩu nhập lại không giống'
			rollback tran
			select -3
			return
		end
		update TaiKhoan
		set pass=@matkhaumoi
		where username=@username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

----------------------------------TIẾP NHẬN-----------------------------
create proc QTV_ThemQuanTriVien
	@username varchar(20),
	@pass varchar(50),
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50), 
	@SDT bigint, 
	@DiaChi nvarchar(50)
as
begin tran
	begin try
		
		if @username='' or @pass='' or @HoTen='' or  @NgaySinh='' or @GioiTinh='' or @Email='' or @SDT='' or @DiaChi=''
		begin
			print N'Có trường thông tin trống'
			rollback tran
			select -1
			return
		end
		if exists(select username from TaiKhoan where username=@username)
		begin 
			print N'Ten tai khoan da ton tai'
			rollback tran
			select -2
			return
		end
		declare @MaQT int
		set @MaQT=0
		if (select count(MaQT) from QuanTri)>0
		begin 
			set @MaQT=(select Max(MaQT) from QuanTri)
		end
		set @MaQT=@MaQT+1
		insert into TaiKhoan values(@username,@pass,1)
		insert into QuanTri values(@MaQT,@HoTen,@NgaySinh,@GioiTinh,@Email,@SDT,@DiaChi,@username)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


create --alter
proc QTV_ThemHocSinh
	@username varchar(20),
	@pass varchar(50),
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50), 
	@SDT bigint, 
	@DiaChi nvarchar(50)
as
begin tran
	begin try
		
		if @username='' or @pass='' or @HoTen='' or  @NgaySinh='' or @GioiTinh='' or @Email='' or @SDT='' or @DiaChi=''
		begin
			print N'Có trường thông tin trống'
			rollback tran
			select -1
			return
		end
		if exists(select username from TaiKhoan where username=@username)
		begin 
			print N'Ten tai khoan da ton tai'
			rollback tran
			select -2
			return
		end
		declare @MaHS int
		set @MaHS=0
		if (select count(MaHS) from HocSinh)>0
		begin 
			set @MaHS=(select Max(MaHS) from HocSinh)
		end
		set @MaHS=@MaHS+1
		insert into TaiKhoan values(@username,@pass,1)
		insert into QuanTri values(@MaHS,@HoTen,@NgaySinh,@GioiTinh,@Email,@SDT,@DiaChi,@username)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO
	


-----------------------------------------------QUẢN TRỊ VIÊN---KHÓA HỌC
create --alter
proc QTV_ThemNamHoc
@Nam varchar(12),
@TuoiToiThieu int,
@TuoiToIDa int
as
begin tran
	begin try
		if @Nam='' 
		begin
			print N'Có thông tin rỗng'
			rollback tran
			select -1
			return
		end
		if exists(select Nam from NamHoc where Nam=@Nam)
		begin
			print N'Năm học đã tồn tại'
			rollback tran
			select -2
			return
		end
		if @TuoiToiDa>20 or @TuoiToiThieu<15 or @TuoiToiDa<@TuoiToiThieu
		begin
			print N'Tuổi bạn nhập không hợp lệ'
			rollback tran
			select -3
			return
		end
		insert into NamHoc values(@Nam,@TuoiToiThieu,@TuoiToiDa)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_ThemLopHoc
@TenLop varchar(10),
@Nam varchar(12),
@SiSo int
as
begin tran
	begin try
		if @Nam='' or @TenLop=''  
		begin
			print N'Có thông tin rỗng'
			rollback tran
			select -1
			return
		end
		if exists(select Nam from Lop where Nam=@Nam and TenLop=@TenLop)
		begin
			print N'Lớp học đã tồn tại'
			rollback tran
			select -2
			return
		end
		if @SiSo<0 or @SiSO>40
		begin
			print N'Sĩ số tối đa bạn nhập không hợp lệ'
			rollback tran
			select -3
			return
		end
		insert into Lop values(@TenLop,@Nam,@SiSo)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


create --alter
proc QTV_ThemMonHoc
@TenMon nvarchar(20),
@Nam varchar(12),
@DiemDat int
as
begin tran
	begin try
		if @Nam='' or @TenMon=''  
		begin
			print N'Có thông tin rỗng'
			rollback tran
			select -1
			return
		end
		if exists(select Nam from MonHoc where Nam=@Nam and TenMon=@TenMon)
		begin
			print N'Môn học đã tồn tại'
			rollback tran
			select -2
			return
		end
		if @DiemDat<0 or @DiemDat>10
		begin
			print N'Điểm đạt bạn nhập không hợp lệ'
			rollback tran
			select -3
			return
		end
		insert into MonHoc values(@TenMon,@Nam,@DiemDat)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_ThemDiemChoHocSinh
	@MaHocSinh int,
	@TenMon nvarchar(20),
	@Nam varchar(12)
as
begin tran
	begin try
		insert into Diem_HocSinh_MonHoc values(@MaHocSinh,@TenMon,@Nam,1,0.0,0.0,0.0)
		insert into Diem_HocSinh_MonHoc values(@MaHocSinh,@TenMon,@Nam,2,0.0,0.0,0.0)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_SuaNamHoc
	@Nam varchar(12),
	@TuoiToiThieu int,
	@TuoiToiDa int
as
begin tran
	begin try
		if @TuoiToiThieu<15 or @TuoiToiDa>20 or @TuoiToiThieu>@TuoiToiDa
		begin
			print N'Tuổi bạn nhập không hợp lệ'
			rollback tran
			select -1
			return
		end
		update NamHoc
		set TuoiToiThieu=@TuoiToiThieu, TuoiToiDa=@TuoiToiDa
		where Nam=@Nam
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO
		

create --alter
proc QTV_SuaLopHoc
	@TenLop varchar(10),
	@Nam varchar(12),
	@SiSo int
as
begin tran
	begin try
		if @SiSo<0 or @SiSo>40 
		begin
			print N'Si so bạn nhập không hợp lệ'
			rollback tran
			select -1
			return
		end
		if @SiSo<(select count(*) from DanhSachLopHoc where Nam=@Nam and TenLop=@TenLop)
		begin
			print N'Si so hien tai cua lop lon hon so ban nhap'
			rollback tran
			select -2
			return
		end
		update Lop
		set SiSo=@SiSo
		where Nam=@Nam and TenLop=@TenLop
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO
		


create --alter
proc QTV_SuaMonHoc
	@TenMon nvarchar(20),
	@Nam varchar(12),
	@DiemDat float
as
begin tran
	begin try
		if @DiemDat<0 or @DiemDat>10
		begin
			print N'DIEM bạn nhập không hợp lệ'
			rollback tran
			select -1
			return
		end
		update MonHoc
		set DiemDat=@DiemDat
		where Nam=@Nam and TenMon=@TenMon
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_XoaNamHoc
	@Nam varchar(12)
as
begin tran
	begin try
		if exists(select Nam from MonHoc where Nam=@Nam) or exists(select Nam from Lop where Nam=@Nam)
		begin
			print N'Không thể xóa'
			rollback tran
			select -1
			return
		end
		delete from NamHoc where Nam=@Nam
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_XoaLopHoc
	@TenLop varchar(10),
	@Nam varchar(12)
as
begin tran
	begin try
		if exists(select Nam from DanhSachLopHoc where TenLop=@TenLop and Nam=@Nam) or exists(select Nam from GiaoVien_LopHoc where TenLop=@TenLop and Nam=@Nam)
		begin
			print N'Không thể xóa'
			rollback tran
			select -1
			return
		end
		delete from Lop where TenLop=@TenLop and Nam=@Nam
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_XoaMonHoc
	@TenMon nvarchar(20),
	@Nam varchar(12)
as
begin tran
	begin try
		if exists(select Nam from Diem_HocSinh_MonHoc where TenMon=@TenMon and Nam=@Nam) 
		begin
			print N'Không thể xóa'
			rollback tran
			select -1
			return
		end
		delete from MonHoc where TenMon=@TenMon and Nam=@Nam
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc QTV_LayBangDiem
@Nam varchar(12),
@TenLop varchar(10),
@Ki int,
@TenMon nvarchar(20)
as
begin tran
	begin try
		if @Nam=''
		begin
			print N'Năm rỗng'
			rollback tran
			select -1
			return
		end
		if @TenLop=''
		begin
			print N'Tên lớp rỗng'
			rollback tran
			select -2
			return
		end
		if @Ki=''
		begin
			print N'Kì rỗng'
			rollback tran
			select -3
			return
		end
		if @TenMon=''
		begin
			print N'Tên môn rỗng'
			rollback tran
			select -4
			return
		end
		
		select MaHS as 'MaHocSinh',HoTen,Diem15,Diem1Tiet,DiemCuoiKi
		from (select* from Diem_HocSinh_MonHoc where Nam=@Nam and TenMon=@TenMon and @Ki=KiHoc) as D,
		(select MaHocSinh from DanhSachLopHoc where Nam=@Nam and TenLop=@TenLop) as M,HocSinh hs
		where D.MaHocSinh=M.MaHocSinh and hs.MaHS=M.MaHocSinh
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO

create function lamtron(@num float)
returns varchar(7)
as
begin
	return replace(cast(round(@num*1000,0)/1000 as varchar),'00000000','')
end
go

create --alter
proc QTV_TongKetMon
@Nam varchar(12),
@KiHoc int
as 
begin tran 
	begin try
		if @nam=''
		begin 
			print N'Vui lòng chọn năm'
			rollback tran
			select -1
			return
		end
		if @KiHoc=''
		begin 
			print N'Vui lòng chọn năm'
			rollback tran
			select -2
			return
		end

		select TenMon,count(MaHocSinh) as 'SiSo',
			(select count(D.MaHocSinh)
					from Diem_HocSinh_MonHoc D
					where D.TenMon =a.TenMon  and @Nam =d.Nam and d.KiHoc = @KiHoc  and (D.Diem15*0.1+D.Diem1Tiet*0.3+D.DiemCuoiKi*0.6)>(select DiemDat from MonHoc where TenMon=a.TenMon and Nam=@Nam) ) as 'SoNguoiDat'
			,dbo.lamtron(((select count(D.MaHocSinh)
						from Diem_HocSinh_MonHoc D
						where D.TenMon =a.TenMon  and @Nam =d.Nam and d.KiHoc = @KiHoc  and (D.Diem15*0.1+D.Diem1Tiet*0.3+D.DiemCuoiKi*0.6)>(select DiemDat from MonHoc where TenMon=a.TenMon  and Nam=@Nam) )*1.0 /count(MaHocSinh)*1.0)*100) as 'TiLeDat'

			,dbo.lamtron(avg((Diem15*0.1+Diem1Tiet*0.3+DiemCuoiKi*0.6))) as 'DTB'
		from Diem_HocSinh_MonHoc a
		where Nam=@Nam and KiHoc=@KiHoc
		group by TenMon
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO

	


			

create --alter
proc QTV_TatCaTongKetMon
as
begin tran 
	begin try
		select Nam,KiHoc,TenMon,count(MaHocSinh) as 'SiSo',
			(select count(D.MaHocSinh)
			from Diem_HocSinh_MonHoc D
			where D.TenMon =a.TenMon  and a.Nam=d.Nam and d.KiHoc =a.KiHoc  and (D.Diem15*0.1+D.Diem1Tiet*0.3+D.DiemCuoiKi*0.6)>(select DiemDat from MonHoc where TenMon=a.TenMon and Nam=a.Nam ) ) as 'SoNguoiDat'
			,dbo.lamtron(((select count(D.MaHocSinh)
			from Diem_HocSinh_MonHoc D
			where D.TenMon =a.TenMon  and a.Nam=d.Nam and d.KiHoc =a.KiHoc  and (D.Diem15*0.1+D.Diem1Tiet*0.3+D.DiemCuoiKi*0.6)>(select DiemDat from MonHoc where TenMon=a.TenMon and Nam=a.Nam ) )*1.0 /count(MaHocSinh)*1.0)*100) as 'TiLeDat'
			,dbo.lamtron(avg((Diem15*0.1+Diem1Tiet*0.3+DiemCuoiKi*0.6))) as 'DTB'
		from Diem_HocSinh_MonHoc a
	group by Nam,KiHoc,TenMon
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO
--Với mỗi lớp, tìm điểm trung bình của từng học sinh lớp đó theo 1 kì nhập vào
create --alter 
proc QTV_LayBangTongKetLop
@nam varchar(12),
@TenLop varchar(10),
@KiHoc nvarchar(20)
as
begin tran 
	begin try
		if @nam=''
		begin 
			print N'Năm học trống'
			rollback tran
			select -1
			return
		end
		if @TenLop=''
		begin 
			print N'Lớp học trống'
			rollback tran
			select -2
			return
		end
		if @KiHoc=''
		begin 
			print N'Kì học trống'
			rollback tran
			select -3
			return
		end
		if @KiHoc='1' or @KiHoc='2'
		begin 
			select Ds.MaHocSinh,Hs.HoTen,Hs.NgaySinh, dbo.lamtron(avg(D.Diem15*0.1+D.Diem1Tiet*0.3+DiemCuoiKi*0.6)) as 'DTB'
			from 
				(select* from DanhSachLopHoc  where TenLop=@TenLop and  Nam=@nam) Ds ,
				(select* from Diem_HocSinh_MonHoc where Nam=@nam) D,HocSinh Hs
			where Ds.MaHocSinh=D.MaHocSinh and KiHoc=@KiHoc and Hs.MaHS=Ds.MaHocSinh
			group by Ds.MaHocSinh,Hs.HoTen,Hs.NgaySinh
		end
		else 
		begin
			select Ds.MaHocSinh,Hs.HoTen,Hs.NgaySinh, dbo.lamtron(avg(D.Diem15*0.1+D.Diem1Tiet*0.3+DiemCuoiKi*0.6)) as 'DTB'
			from 
				(select* from DanhSachLopHoc  where TenLop=@TenLop and  Nam=@nam) Ds ,
				(select* from Diem_HocSinh_MonHoc where Nam=@nam) D,HocSinh Hs
			where Ds.MaHocSinh=D.MaHocSinh and Hs.MaHS=Ds.MaHocSinh
			group by Ds.MaHocSinh,Hs.HoTen,Hs.NgaySinh
		end
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO

---------------------------------------HỌC SINH------------------------------------------
------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------
------------------------------------------

create --alter
proc HocSinh_CapNhatThongTinCaNhan
	@MaHS int,
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50), 
	@SDT bigint, 
	@DiaChi nvarchar(50)
as
begin tran
	begin try
		if @HoTen='' or  @NgaySinh='' or @GioiTinh='' or @Email='' or @SDT='' or @DiaChi=''
		begin
			print N'Có trường thông tin trống'
			rollback tran
			select -1
			return
		end
		update HocSinh
		set HoTen=@HoTen,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,Email=@Email,SDT=@SDT,DiaChi=@DiaChi
		where MaHS=@MaHS
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


create proc HocSinh_LayThongTin
@username varchar(20)
as
begin 
	select* from HocSinh where username=@username
end
go



create --alter
proc Student_BangDiem
	@mahs int,
	@Nam varchar(12),
	@KiHoc int
as
begin tran
	begin try
		if @Nam=''
		begin
			print N'Chua nhap nam'
			rollback tran
			select -1
			return
		end
		if @KiHoc=''
		begin
			print N'Chua nhap ki'
			rollback tran
			select -2
			return
		end
		select TenMon,Diem15,Diem1Tiet,DiemCuoiKi,dbo.lamtron(Diem15*0.1+Diem1Tiet*0.3+DiemCuoiKi*0.6) as 'DiemTongKetMon'
		from Diem_HocSinh_MonHoc 
		where @Nam=Nam and @KiHoc =KiHoc and @mahs=MaHocSinh
		Group by TenMon,Diem15,Diem1Tiet,DiemCuoiKi
		
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO
create proc Student_BangDiem_TongHop
	@mahs int
as
begin 
	select * from Diem_HocSinh_MonHoc where @mahs=MaHocSinh
end
go

create --alter
proc HocSinh_DanhSachLop
	@mahs int,
	@Nam varchar(12)
as
begin tran
	begin try
		if @Nam=''
		begin 
			print N'chọn năm'
			rollback tran
			select -1
			return
		end
		select Nam,TenLop,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi from 
		DanhSachLopHoc,HocSinh
		where Nam=@Nam and
		TenLop=(select TenLop from DanhSachLopHoc where MaHocSinh=@mahs and Nam=@Nam) and MaHocSinh=MaHS
		end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO

		

--------------------------------------ROLE: GIAOVIEN-----------------------------------
------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------
create --alter
proc GV_LayThongTin
	@username varchar(20)
as
begin
	select* from GiaoVien where username=@username
end
go

create -- alter 
proc GV_danhsachhslop @Nam varchar(12),@TenLop varchar(10)
as
begin try
	select MaHS,HoTen,NgaySinh,GioiTinh,Email,dbo.SDT_To_Char(SDT) SDT,DiaChi
	from HocSinh
	where MaHS in (select MaHocSinh from DanhSachLopHoc where TenLop = @TenLop and @Nam = Nam)
	
end try
begin catch
	select -10
end catch
go
insert into GiaoVien_LopHoc values(1,'10A1','2019-2020')
insert into GiaoVien_LopHoc values(1,'11A1','2019-2020')

create --alter
proc GV_LayLopDay
	@MaGV int
as
begin 
	select (Nam+' / '+TenLop) as 'Lop'
	from GiaoVien_LopHoc
	where MaGV=@MaGV
	order by Nam ASC,TenLop ASC
end
go



create --alter
proc GV_CapNhatThongTinCaNhan
	@MaGV int,
	@HoTen nvarchar(50),
	@NgaySinh date,
	@GioiTinh nvarchar(10),
	@Email varchar(50), 
	@SDT bigint, 
	@DiaChi nvarchar(50)
as
begin tran
	begin try
		if @HoTen='' or  @NgaySinh='' or @GioiTinh='' or @Email='' or @SDT='' or @DiaChi=''
		begin
			print N'Có trường thông tin trống'
			rollback tran
			select -1
			return
		end
		update GiaoVien
		set HoTen=@HoTen,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,Email=@Email,SDT=@SDT,DiaChi=@DiaChi
		where MaGV=@MaGV
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


		
create --alter
proc GV_LayBangDiem
	@Nam varchar(12),
	@TenLop varchar(10),
	@Ki nvarchar(20),
	@TenMon nvarchar(30)
as
begin tran
	begin try
		if @Nam=''
		begin
			print N'Chưa chọn năm'
			rollback tran
			select -1
			return
		end
		if @TenLop=''
		begin
			print N'Chưa chọn lớp'
			rollback tran
			select -2
			return
		end
		if @Ki='1' or @Ki='2'
		begin
			select H.MaHS,H.HoTen,D.KiHoc,D.Diem15,D.Diem1Tiet,D.DiemCuoiKi
			from (select * from Diem_HocSinh_MonHoc where Nam=@Nam and TenMon=@TenMon and KiHoc=@Ki) D,
				(select* from DanhSachLopHoc where Nam=@Nam and TenLop=@TenLop) L,
				HocSinh H
			where H.MaHS=L.MaHocSinh and D.MaHocSinh=L.MaHocSinh
		end
		else
		begin
			select H.MaHS,H.HoTen,D.KiHoc,D.Diem15,D.Diem1Tiet,D.DiemCuoiKi
			from (select * from Diem_HocSinh_MonHoc where Nam=@Nam and TenMon=@TenMon) D,
				(select* from DanhSachLopHoc where Nam=@Nam and TenLop=@TenLop) L,
				HocSinh H
			where H.MaHS=L.MaHocSinh and D.MaHocSinh=L.MaHocSinh
		end
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO

create --alter
proc GV_CapNhatDiem
	@Nam varchar(12),
	@Ki nvarchar(20),
	@TenMon nvarchar(30),
	@MaHS int,
	@Diem15 float,
	@Diem1Tiet float,
	@DiemCuoiKi float
as
begin tran
	begin try
		if @Diem15>10 or @Diem15<0 or @Diem1Tiet>10 or @Diem1Tiet<0 or @DiemCuoiKi>10 or @DiemCuoiKi<0 
		begin
			print N'Điểm không hợp lệ'
			rollback tran
			select -1
			return
		end
		update Diem_HocSinh_MonHoc
		set Diem15=@Diem15,Diem1Tiet=@Diem1Tiet,DiemCuoiKi=@DiemCuoiKi
		where Nam=@Nam and KiHoc=@Ki and MaHocSinh=@MaHS and TenMon=@TenMon
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO




create -- alter
proc GV_DiemTrungBinh
@Nam varchar(12),
@TenLop varchar(10),
@Ki nvarchar(10),
@TenMon nvarchar(20)
as
begin tran
	begin try
		if @Nam=''
		begin
			print N'Năm rỗng'
			rollback tran
			select -1
			return
		end
		if @TenLop=''
		begin
			print N'Tên lớp rỗng'
			rollback tran
			select -2
			return
		end
		if @Ki=''
		begin
			print N'Kì rỗng'
			rollback tran
			select -3
			return
		end
		if @TenMon=''
		begin
			print N'Tên môn rỗng'
			rollback tran
			select -4
			return
		end
		if (@Ki='1' or @Ki='2')
			select MaHS as 'MaHocSinh',HoTen,hs.NgaySinh,dbo.lamtron(Diem15*0.1+Diem1Tiet*0.3+DiemCuoiKi*0.6) as 'DTB'
			from (select* from Diem_HocSinh_MonHoc where Nam=@Nam and TenMon=@TenMon and @Ki=KiHoc) as D,
			(select MaHocSinh from DanhSachLopHoc where Nam=@Nam and TenLop=@TenLop) as M,HocSinh hs
			where D.MaHocSinh=M.MaHocSinh and hs.MaHS=M.MaHocSinh
		else
			--select MaHS as 'MaHocSinh',HoTen,hs.NgaySinh,dbo.lamtron(avg(Diem15*0.1+Diem1Tiet*0.3+DiemCuoiKi*0.6)) as 'DTB'
			--from (select* from Diem_HocSinh_MonHoc where Nam=@Nam and TenMon=@TenMon) as D,
			--(select MaHocSinh from DanhSachLopHoc where Nam=@Nam and TenLop=@TenLop) as M,HocSinh hs
			--where D.MaHocSinh=M.MaHocSinh and hs.MaHS=M.MaHocSinh
			select Ds.MaHocSinh,Hs.HoTen,Hs.NgaySinh, dbo.lamtron(avg(D.Diem15*0.1+D.Diem1Tiet*0.3+DiemCuoiKi*0.6)) as 'DTB'
			from 
				(select* from DanhSachLopHoc  where TenLop=@TenLop and  Nam=@nam) Ds ,
				(select* from Diem_HocSinh_MonHoc where Nam=@nam and TenMon=@TenMon) D,HocSinh Hs
			where Ds.MaHocSinh=D.MaHocSinh and Hs.MaHS=Ds.MaHocSinh
			group by Ds.MaHocSinh,Hs.HoTen,Hs.NgaySinh
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO




create -- alter
proc GV_TongKetMon
	@TenMon nvarchar(12)
as
begin tran 
	begin try
		select Nam,KiHoc,TenMon,count(MaHocSinh) as 'SiSo',
			(select count(D.MaHocSinh)
			from Diem_HocSinh_MonHoc D
			where D.TenMon =a.TenMon  and a.Nam=d.Nam and d.KiHoc =a.KiHoc  and (D.Diem15*0.1+D.Diem1Tiet*0.3+D.DiemCuoiKi*0.6)>(select DiemDat from MonHoc where TenMon=a.TenMon and Nam=a.Nam ) ) as 'SoNguoiDat'
			,dbo.lamtron(((select count(D.MaHocSinh)
			from Diem_HocSinh_MonHoc D
			where D.TenMon =a.TenMon  and a.Nam=d.Nam and d.KiHoc =a.KiHoc  and (D.Diem15*0.1+D.Diem1Tiet*0.3+D.DiemCuoiKi*0.6)>(select DiemDat from MonHoc where TenMon=a.TenMon and Nam=a.Nam ) )*1.0 /count(MaHocSinh)*1.0)*100) as 'TiLeDat'
			,dbo.lamtron(avg((Diem15*0.1+Diem1Tiet*0.3+DiemCuoiKi*0.6))) as 'DTB'
		from Diem_HocSinh_MonHoc a
		where @TenMon =TenMon
		group by Nam,KiHoc,TenMon
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO



create -- alter 
proc DangNhap @username varchar(20),@password varchar(50)
as
begin try
	if @username = '' or @password = ''
	begin
		select -1
		return
	end
	if CHARINDEX(' ',trim(' ' from @username)) != 0 
	begin
		select -2
		return
	end
	if CHARINDEX(' ',trim(' ' from @password)) != 0 
	begin
		select -3
		return
	end
	declare @loai int
	set @loai = (select Loai from TaiKhoan where @username = username and @password = pass)
	if @loai is null
	begin
		select -4
		return
	end
	select @loai
	return
end try
begin catch
	--Lỗi hệ thông
	select 0
	return
end catch
go

create --alter
proc QTV_ThemDiemHocSinh
@MaHS int,
@TenMon nvarchar(30),
@Nam varchar(12)
as
begin tran
	begin try
		insert into Diem_HocSinh_MonHoc values(@MaHS,@TenMon,@Nam,1,0,0,0)
		insert into Diem_HocSinh_MonHoc values(@MaHS,@TenMon,@Nam,2,0,0,0)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


create --alter
proc QTV_XoaHocSinhDayDu
	@MaHS int
as
begin tran
	begin try
		delete from DanhSachLopHoc where MaHocSinh=@MaHS
		delete from Diem_HocSinh_MonHoc where MaHocSinh=@MaHS
		delete from HocSinh where MaHS=@MaHS
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO