drop database if exists PetStore;

create database PetStore;

use PetStore;

create table if not exists Customers(
	customer_id int auto_increment primary key,
    customer_name varchar(100) not null,
    customer_phonenumber varchar (13) not null
);

create table if not exists Staffs(
	staff_id int primary key auto_increment,
    staff_name varchar(100) not null,
    staff_username varchar(50) not null unique,
    staff_password varchar(50) not null,
    staff_phonenumber varchar(13) not null unique,
    staff_address  varchar(100),
    staff_gmail  varchar(100) not null unique,
    staff_role int not null default 2 -- 1.StoreManager; 2.Cashier --
);
						
insert into Staffs(staff_name, staff_username, staff_password, staff_role, staff_phonenumber, staff_gmail)
values ('Ngo Quang Nguyen', 'nguyen2504', 'acdb072852b204c0f3dede27a1845527', 1, '0358516683', 'ngoquangnguyen2504@gmail.com');
									

create table if not exists Categories(
	category_id int auto_increment primary key,
    category_name nvarchar(100) not null
);

create table if not exists Brands(
	brand_id int auto_increment primary key,
    brand_name varchar(100) not null
);

create table if not exists Items(
	item_id int auto_increment primary key,
    item_name nvarchar(200) not null,
    item_brand int not null,
    item_price double not null,
	item_weight nvarchar(10),
    item_quantity int not null,
    item_category int not null,
    item_description nvarchar(500),
    foreign key (item_category) references Categories(category_id),
    foreign key (item_brand) references Brands(brand_id)
);

insert into Categories (category_name)
values ('Thuc an'),
	   ('Phu kien'),
       ('Thuoc thu y'),
       ('Vat dung');

insert into Brands (brand_name)
values ('WHISKAS'),
	   ('ROYAL CANIN'),
       ('PAW'),
       ('MERIAL'),
       ('JOYCE & DOLLS'),
       ('TRIXIE'),
       ('BOBO'),
       ('AUPET'),
       ('BIOLINE'),
       ('TROPICLEAN'),
       ('VEGEBRAND');

insert into Items (item_name, item_brand, item_price, item_weight, item_quantity, item_category, item_description)
values ('Pate cho meo vi ca ngu WHISKAS Tuna Flavour Sauce', 1, 25000, '85g', 254, 1, 'Pate cho meo vi ca ngu WHISKAS Tuna Flavour Sauce voi nhieu huong vi thom ngon dac trung danh rieng cho meo. Thuc pham co tac dung can bang dinh duong hang ngay, lua chon nhung thanh phan thit – ca tuoi ngon nhat trong che bien, giau protein, cac vitamin & khoang chat thiet yeu va khong co chat bao quan.'),
	   ('Pate cho meo vi thit bo WHISKAS Beef Flavour Sauce', 1, 25000, '85g', 500, 1, 'Pate cho meo vi nuoc sot thit bo WHISKAS Beef Flavour Sauce thom ngon dac trung danh rieng cho meo, giup meo cung an uong ngon mieng hon. Tang mien dich, ho tro tieu hoa. Cham soc long mem muot va han che toi da ty le rung long hang nam cua meo cung.'),
	   ('Pate cho meo vi thit ga WHISKAS Chicken Flavour Sauce', 1, 25000, '85g', 558, 1, 'Nuoc sot cho meo vi thit ga WHISKAS Chicken Flavour in Sauce voi huong vi thom ngon dac trung danh rieng cho meo. San pham mang toi bua an ngon moi ngay cho meo cung giau protein tu thit ga cung cap nguon nang luong toi da. Ho tro tieu hoa. Thuc day qua trinh trao doi chat.'),
       ('Pate cho meo vi ca bien WHISKAS Ocean Fish Flavour Sauce', 1, 25000, '85g', 615, 1, 'Pate cho meo vi nuoc sot ca bien WHISKAS Ocean Fish Flavour Sauce thom ngon dac trung danh rieng cho meo. Gop phan tao ra nhung bua an ngon cho meo cung. Bo sung protein, dam va khoang chat. Ho tro he tieu hoa. Cham soc long va da manh khoe.'),
	   ('Pate cho meo vi gan WHISKAS Liver Flavour Sauce', 1, 25000, '85g', 745, 1, 'Nuoc sot cho meo vi gan WHISKAS Liver Flavour in Sauce thom ngon dac trung danh rieng cho meo mang toi mon an ngon mieng cho meo cung. Giup an ngon hon. Ho tro he tieu hoa. Cham soc long va da luon khoe manh.'),
       ('Pate cho meo vi bach tuoc WHISKAS Octopus Flavour Sauce', 1, 25000, '85g', 135, 1, 'Pate cho meo vi nuoc sot bach tuoc WHISKAS Octopus Flavour Sauce dac trung danh rieng cho meo. Thuc pham co tac dung can bang dinh duong hang ngay, lua chon nhung thanh phan thit – ca tuoi ngon nhat trong che bien, giau protein, cac vitamin & khoang chat thiet yeu va khong co chat bao quan.'),                
       ('Thuc an cho cho con ROYAL CANIN Mini Puppy 800g', 2, 185000, '800g', '205', 1, 'Thuc an cho cho con ROYAL CANIN Mini Puppy danh cho cac giong cho con duoi 10 thang tuoi. Voi cong thuc dac che rieng cho nhu cau dinh duong cua cho con. Thuc an cho cho con duoc nghien cuu de cung cap dinh duong theo nhu cau thuc te cua cho con.'),
       ('Thuc an cho cho con ROYAL CANIN Mini Puppy(2kg)', 2, 390000, '2kg', '150', 1, 'Thuc an cho cho con ROYAL CANIN Mini Puppy danh cho cac giong cho con duoi 10 thang tuoi. Voi cong thuc dac che rieng cho nhu cau dinh duong cua cho con. Thuc an cho cho con duoc nghien cuu de cung cap dinh duong theo nhu cau thuc te cua cho con.'),
       ('Thuc an cho cho con ROYAL CANIN Mini Puppy(8kg)', 2, 1340000, '8kg', '98', 1, 'Thuc an cho cho con ROYAL CANIN Mini Puppy danh cho cac giong cho con duoi 10 thang tuoi. Voi cong thuc dac che rieng cho nhu cau dinh duong cua cho con. Thuc an cho cho con duoc nghien cuu de cung cap dinh duong theo nhu cau thuc te cua cho con.'),
       ('Thuc an cho cho truong thanh ROYAL CANIN Maxi Adult(1kg)', 2, 150000, '1kg', 374, 1, 'Thuc an cho cho truong thanh ROYAL CANIN Maxi Adult. Thuong duoc dung cho cac giong cho 15 thang tuoi tro len. Voi cong thuc dac che rieng cho nhu cau dinh duong cua cho truong thanh. San pham duoc nghien cuu de cung cap dinh duong theo nhu cau thuc te cua cho truong thanh.'),
       ('Thuc an cho cho truong thanh ROYAL CANIN Maxi Adult(4kg)', 2, 525000, '4kg', 341, 1, 'Thuc an cho cho truong thanh ROYAL CANIN Maxi Adult. Thuong duoc dung cho cac giong cho 15 thang tuoi tro len. Voi cong thuc dac che rieng cho nhu cau dinh duong cua cho truong thanh. San pham duoc nghien cuu de cung cap dinh duong theo nhu cau thuc te cua cho truong thanh.'),
       ('Thuc an cho cho truong thanh ROYAL CANIN Maxi Adult(10kg)', 2, 1160000, '10kg', 137, 1, 'Thuc an cho cho truong thanh ROYAL CANIN Maxi Adult. Thuong duoc dung cho cac giong cho 15 thang tuoi tro len. Voi cong thuc dac che rieng cho nhu cau dinh duong cua cho truong thanh. San pham duoc nghien cuu de cung cap dinh duong theo nhu cau thuc te cua cho truong thanh.'),
       ('Thuc an cho meo con ROYAL CANIN Kitten(400g)', 2, 100000, '400g', 587, 1, 'Thuc an cho meo con ROYAL CANIN Kitten bao gom protein tu long trang trung Probiotic, chat chong oxy hoa giup nang cao suc khoe, tang suc de khang, bo sung dinh duong cho meo, giup co the meo phat trien day du. Men tieu hoa Probiotic ho tro tieu hoa cho meo, giup can bang loi khuan trong duong ruot. Chat xo hoa tan FOS chat lam ngot tu nhien duy, tri can bang he vi sinh duong ruot cua meo.'),
       ('Thuc an cho meo con ROYAL CANIN Kitten(2kg)', 2, 410000, '2kg', 247, 1, 'Thuc an cho meo con ROYAL CANIN Kitten bao gom protein tu long trang trung Probiotic, chat chong oxy hoa giup nang cao suc khoe, tang suc de khang, bo sung dinh duong cho meo, giup co the meo phat trien day du. Men tieu hoa Probiotic ho tro tieu hoa cho meo, giup can bang loi khuan trong duong ruot. Chat xo hoa tan FOS chat lam ngot tu nhien duy, tri can bang he vi sinh duong ruot cua meo.'),
       ('Thuc an cho meo con ROYAL CANIN Kitten(10kg)', 2, 1715000, '10kg', 58, 1, 'Thuc an cho meo con ROYAL CANIN Kitten bao gom protein tu long trang trung Probiotic, chat chong oxy hoa giup nang cao suc khoe, tang suc de khang, bo sung dinh duong cho meo, giup co the meo phat trien day du. Men tieu hoa Probiotic ho tro tieu hoa cho meo, giup can bang loi khuan trong duong ruot. Chat xo hoa tan FOS chat lam ngot tu nhien duy, tri can bang he vi sinh duong ruot cua meo.'),
       ('Thuc an cho meo truong thanh ROYAL CANIN Indoor 27(400g)', 2, 105000, '400g', 1082, 1, 'Thuc an cho meo truong thanh ROYAL CANIN Indoor 27 danh cho tat ca giong meo song trong nha tren 12 thang.'),
       ('Thuc an cho meo truong thanh ROYAL CANIN Indoor 27(2kg)', 2, 425000, '2kg', 519, 1, 'Thuc an cho meo truong thanh ROYAL CANIN Indoor 27 danh cho tat ca giong meo song trong nha tren 12 thang.'),
       ('Thuc an cho meo truong thanh ROYAL CANIN Indoor 27(10kg)', 2, 1785000, '10kg', 132, 1, 'Thuc an cho meo truong thanh ROYAL CANIN Indoor 27 danh cho tat ca giong meo song trong nha tren 12 thang.'),
       ('Dung cu an uong cho cho meo PAW tu dong da nang', 3, 430000, '1.2kg', 10, 4, 'Dung cu an uong cho cho meo PAW tu dong da nang la san pham danh cho tat ca giong cho va meo.'),
       ('Do cao mong cho meo PAW Cat Scratch Pan', 3, 220000, '0.552kg', 25, 2, 'Do cao mong cho meo PAW Cat Scratch Pan la san pham danh cho tat ca giong meo.'),
       ('Roi huan luyen cho meo PAW', 3, 50000, '0.1kg', 5, 2, 'Roi huan luyen cho meo PAW voi thiet ket voi chat lieu cao su mem, than roi duoc ben bang day du va ben trong co kim loai. Co tinh dan hoi cao.'),
       ('Nha nhua cho cho meo co mai hien PAW Plastic House', 3, 2500000, '10kg', 2, 2, 'Nha nhua cho cho meo co mai hien PAW Plastic House co lon phu hop voi tat ca giong cho va meo. San pham co the de trong nha hoac ngoai troi.'),
	   ('Kem cat bam kem dua mong cho meo PAW Claw Clipper & File', 3, 110000, '300g', 5, 4, 'Kem cat bam kem dua mong cho meo PAW Claw & Clipper File duoc dung de cat mong cho thu cung. San pham co kem theo dua mong. Mau sac va thiet ke cua san pham co the thay doi khac nhau theo tung lo hang.'),
       ('Thuoc nho gay tri ve ran cho cho duoi 10kg MERIAL Frontline Plus', 4, 210000, '0.67ml', 44, 3, 'Thuoc nho gay tri ve ran cho cho duoi 10kg MERIAL Frontline Plus. Phu hop voi tat ca cac giong cho co trong luong duoi 10kg va tu 2 thang tuoi tro len. Co the su dung cho cho dang mang thai va cho con bu.'),
       ('Thuoc nho gay tri ve ran cho cho 10 20kg MERIAL Frontline Plus', 4, 230000, '1.67ml', 30, 3, 'Thuoc nho gay tri ve ran cho cho 10-20kg MERIAL Frontline Plus Danh cho cac giong cho lon va trung binh. Dac biet an toan co the su dung cho cho mang thai va cho con bu.'),
	   ('Thuoc nho gay tri ve ran cho cho 20-40kg MERIAL Frontline Plus', 4, 250000, '2.68ml', 15, 3, 'Thuoc nho gay tri ve ran cho cho 20-40kg MERIAL Frontline Plus la san pham danh cho tat ca giong cho tu 20 den 40kg. Dac biet an toan, co the su dung cho cho mang thai va cho con bu.'),
       ('Thuoc nho gay tri ve ran cho meo MERIAL Frontline Plus', 4, 210000, '0.5ml', 105, 3, 'Thuoc nho gay tri ve ran cho meo MERIAL Frontline Plus duoc su dung duoc cho meo tu 2 thang tuoi tro len. San pham co kha nang diet sach tat ca cac loai bo chet lon, trung bo chet, au trung va bo ve khong the tron thoat.'),
       ('Thuoc xit tri ve ran cho cho meo MERIAL Frontline Spray Treatment', 4, 250000, '0.1kg', 58, 3, 'Thuoc xit tri ve ran cho cho meo MERIAL Frontline Spray Treatment duoc bac si thu y tin dung. Voi hon 100 quoc gia dang ky su dung, tieu thu hon 1 ty chai. Thuoc co duoc tinh nhe nhang khong ngam vao mau. Vi the an toan su dung cho cho meo. Bao gom cho dang cho con bu va cho meo con. Khong lam hai den thu cung va nguoi. Su dung lau dai co the lam sach sau bo hoan toan.'),
       ('Binh sua cho cho meo so sinh BOBO Pet Milk Bottle', 7, 50000, '0.05kg', 48, 4, 'Binh sua cho cho meo so sinh BOBO Pet Milk Bottle duoc dung cho tat ca giong cho va meo.'),
	   ('Xit bo tat xau cho meo JOYCE & DOLLS Pet Repellent Spray', 5, 120000, '0.15kg', 25, 4, 'Xit bo tat xau cho meo JOYCE DOLLS Pet Repellent Spray la mot san pham tuyet voi cho viec huan luyen cho meo khoi nhung thoi hu tat xau chang han nhu: ngu tren giuong, tham & ghe sofa; can pha giay dep va cac do dac trong nha; va dao tao chung hinh thanh thoi quen khong di ve sinh bua bai trong khu vuc su dung san pham.'),
       ('Xit huong dan cho di ve sinh dung cho JOYCE & DOLLS Trainer Puppy', 5, 150000, '0.5kg', 4, 4, 'Chai nuoc xit huong dan cho di ve sinh dung cho JOYCE & DOLLS Trainer Puppy la 1 cong cu huan luyen tuyet voi phu hop cho cac chu cho di ve sinh dung cho va hinh thanh cho chung mot thoi quen tot trong tuong lai. Tac dung chinh cua thuoc xit co tac dung de huong dan cho di ve sinh dung cho. Hoan toan vo hai voi moi truong,thich hop cho cac nhu cau huan luyen, dao tao cun cung.'),
       ('Tui don phan va chat thai cho meo AUPET Poop Bags', 8, 150000, '0.5kg', 158, 4, 'Tui don phan va chat thai cho meo AUPET Poop Bags duoc dung de don dep “chien tich” cua thu cung. Hay the hien minh la mot nguoi nuoi thu cung van minh – lich su khi thu cung hanh dong khong nhu mong muon noi cong cong.'),
       ('Khan tam cho cho meo sieu tham hut PAW Pet Absorbent Hair Towel', 3, 90000, '0.3kg', 64, 4, 'Khan tam cho cho meo sieu tham hut PAW Pet Absorbent Hair Towel la san pham khong the thieu cho cac ban nuoi cho meo long dai. Day la khan sieu tham nuoc san xuat tren day chuyen cong nghe dac biet. Khong giong voi cac khan bong thong thuong khac.'),
       ('Ban cha tam cho cho meo BOBO Pet Washing Brush', 7, 30000, '0.065kg', 2, 4, 'Ban cha tam cho cho meo BOBO Pet Washing Brush duoc dung cho tat ca giong cho va meo.'),
       ('Kem danh rang cho cho TRIXIE Zahnpflege-Set', 6, 180000, '0.1kg', 8, 4, 'Kem danh rang cho cho TRIXIE Zahnpflege=Set voi huong vi bac ha khu mui hoi cho cho hieu qua, lam sach khoang mieng va khu mui hoi, ngan ngua sau rang toi da. San pham duoc san xuat boi TRIXIE – nhan hieu di dau trong nganh do dung danh cho thu cung tai Duc va Chau Au.'),
       ('Nuoc xit khu trung diet khuan cho cho meo JOYCE & DOLLS', 5, 150000, '0.5kg', 2, 4, 'Nuoc xit khu trung diet khuan cho cho meo JOYCE & DOLLS Pet Family Intensive Disinfectant co tac dung thanh trung cac loai vi khuan, mam benh truyen nhiem tren co the cua cho meo.'),
       ('Xit khu mui hoi cho meo huong chanh JOYCE & DOLLS', 5, 190000, '0.5kg', 9, 4, 'Xit khu mui hoi cho meo huong chanh JOYCE & DOLLS Pet Family Lemon Scent Spray duoc dung cho tat ca giong cho va meo.'),
       ('Vong co chong liem cho cho meo PAW Pet Elizabethan Collar', 3, 50000, '0.025kg', 158, 4, 'Vong co chong liem cho cho meo PAW Pet Elizabethan Collar duoc dung cho tat ca giong cho va meo.'),
       ('Kep hot phan cho meo PAW Pet Pick Up Feces Clip', 3, 80000, '0.15kg', 18, 4, 'Kep hot phan cho meo PAW Pet Pick Up Feces Clip duoc dung de don sach chien tich cua thu cung de lai trong nha va ngoai san.'),
       ('Dung dich ve sinh tai cho cho meo TRIXIE Ear Care', 6, 170000, '0.05kg', 2, 4, 'Dung dich ve sinh tai cho cho meo TRIXIE Ear Care duoc su dung cho tat ca giong cho va meo.'),
       ('Chai xit thom mieng cho cho TRIXIE Zahnpflege Spray.', 6, 170000, '0.01kg', 7, 4, 'Chai xit thom mieng cho cho TRIXIE Zahnpflege Spray nhap khau truc tiep tu Duc co tac dung lam sach rang, nuou khoe manh va khong con mui hoi mieng. San pham phu hop voi tat ca cac giong cho. Nha san xuat de nghi san pham nay co the ap dung duoc cho ca meo.'),
       ('Cay lan long cho meo dinh vao quan ao BIOLINE Pet Roller', 9, 45000, '0.1kg', 199, 4, 'Cay lan long cho meo dinh vao quan ao BIOLINE Pet Roller la dung cu giup loai bo long cua thu cung tren quan ao hay bat cu cho nao.'),
       ('Nuoc nho mat cho cho meo TRIXIE Augenpflege', 6, 170000, '0.05kg', 54, 4, 'Nuoc nho mat cho cho meo TRIXIE Augenpflege duoc dung cho tat ca giong cho va meo.'),
       ('Nuoc hoa cho cho meo JOYCE DOLLS N°1 L’Eau D’Issey', 5, 280000, '30ml', 7, 4, 'Nuoc hoa cho cho meo JOYCE DOLLS N°1 L’Eau D’Issey Pet Eau De Toilette Spray duoc dung cho tat ca giong cho va meo.'),
       ('Bo ve sinh rang mieng cho cho TROPICLEAN', 10, 155000, '0.015kg', 4, 4, 'Bo ve sinh rang mieng cho cho TROPICLEAN Fresh Breath Dental Trial Kit danh cho tat ca cac giong cho meo.'),
       ('Ban cat tia long cho cho meo PAW Grooming Table', 3, 2900000, '12kg', 0, 4, 'Ban cat tia long cho cho meo PAW Grooming Table ho tro cat tia long chuyen dung, su dung trong viec huan luyen, cat tia long, chai chuot va lam dep cho thu cung cua ban.'),
       ('Hop dung tui nilon boc phan cho meo AUPET Paw Waste Bag', 8, 40000, '0.08kg', 17, 4, 'Hop dung tui nilon boc phan cho meo AUPET Paw Waste Bag. Chat lieu nhua nhe, rat tien loi khi mang theo luc dat thu cung, di dao di choi.'),
       ('Nuoc suc mieng va duong long cho cho meo TROPICLEAN', 10, 295000, '0.473kg', 4, 4, 'Nuoc suc mieng va duong long cho cho meo TROPICLEAN Fresh Breath Water Additive for Skin Coat danh cho tat ca cac giong cho meo.'),
       ('Xuong Canxi cho cho VEGEBRAND Orgo High Calcium Cheese', 11, 50000, '0.09kg', 154, 1, 'Xuong Canxi cho cho VEGEBRAND Orgo High Calcium Cheese co ham luong canxi cao, dap ung tot cho moi giong cho o moi do tuoi khac nhau. Voi thanh phan pho mat chat luong cao tu nguyen lieu tho cung cong thuc sua thom ngon co ham luong canxi cao giup cho su phat trien xuong tot hon, rang chac khoe, loai bo mang bam cao rang, giup rang trang hon, khong gay mui hoi kho chiu va cai thien kha nang gam – nhai cua cun cung.');
	
select *From Staffs;

create table Invoices(
	invoice_id int auto_increment primary key,
    invoice_date datetime default current_timestamp() not null,
    invoice_status int, -- 1: Create New Invoice , 2: Progressing
    staff_id int,
    customer_id int,
    foreign key (staff_id) references Staffs(staff_id),
    foreign key (customer_id) references Customers(customer_id)
);
    
create table InvoiceDetails(
	invoice_id int not null,
    item_id int not null,
    quantity int,
    unit_price double,	
    primary key (invoice_id, item_id),
    foreign key (invoice_id) references Invoices(invoice_id),
    foreign key (item_id) references Items(item_id)
);

create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on PetStore.* to 'vtca'@'localhost';
																					
-- select *from Customers;

-- select *from Staffs;

-- select *from Brands;

-- insert into Brands (brand_name) values('nguyen');

-- select *from InvoiceDetails;

-- select *from Customers where customer_phonenumber = '0123456789';

-- SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_id like 1;

-- SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id;

-- insert into Items (item_name, item_price, item_brand, item_category, item_quantity, item_weight, item_description) 
-- values('"Thuc an cho meo"', 78000, 6, 1, 500, '1kg', 'description');

-- SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_id = '';

