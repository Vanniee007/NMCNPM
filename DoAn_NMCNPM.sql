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

insert into TaiKhoan values('hs01','123',3)
insert into TaiKhoan values('hs02','123',3)

insert into TaiKhoan values('hs03','123',3)

insert into TaiKhoan values('hs04','123',3)


insert into GiaoVien values(1,N'Anh','1-1-1978',N'Nam','abc@gmail.com','0123856789',N'Phus Tho',N'Toán','gv01')
insert into GiaoVien values(2,N'Anh Tú','1-1-1978',N'Nam','abd@gmail.com','0123756789',N'Phus Tho',N'Toán','gv02')
insert into GiaoVien values(3,N'Anh Phạm','1-1-1978',N'Nam','abe@gmail.com','0123454789',N'Phus Tho',N'Văn','gv03')

insert into QuanTri values(1,N'Anh Phạm','1-1-1978',N'Nam','abw@gmail.com','0123454789',N'Phus Tho','admin')
insert into QuanTri values(2,N'Anh Phạm','1-1-1978',N'Nam','abq@gmail.com','0123454789',N'Phus Tho','admin01')
insert into QuanTri values(3,N'Anh Phạm','1-1-1978',N'Nam','adc@gmail.com','0123454789',N'Phus Tho','admin02')

insert into HocSinh values(1,N'Anh Phạm Quy','1-1-2000',N'Nam','hs01@gmail.com','0123454789',N'Phus Tho','hs01')
insert into HocSinh values(2,N'Mai Quyết ','1-1-2000',N'Nam','hs02@gmail.com','0123454789',N'Bình Dương','hs02')
insert into HocSinh values(3,N'Trương Anh Ngọc','1-1-2000',N'Nam','hs03@gmail.com','0123454789',N'Bình Dương','hs03')
insert into HocSinh values(4,N'Lê Thị Vy','1-1-2000',N'Nữ','hs04@gmail.com','0123454789',N'Bình Dương','hs04')
go
---------------------------QUẢN TRỊ VIÊN---------------------------
---------------------------------------------------------------------------------
---------------------------------------------------------------------------------
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
--Exec QTV_LayThongTinTaiKhoan 'admin'
create proc QTV_LayThongTin
@username varchar(20)
as
begin 
	select* from QuanTri where username=@username
end
Exec QTV_LayThongTin 'admin'
	
go
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
Exec QTV_CapNhatThongTin '1',N'Anh Phạm Tieens','1/4/1978','Nam','abw@gmail.com','123454789','Phus Tho'
go
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
	
Exec QTV_ThemQuanTriVien 'admin10','123',N'Lê Min Tian','12/15/2022','Nam','@gmail.com','12345','1234'

	






	


	
