# Sodexo Final Project

> Tamamlad���m Bu WebAPI projesinde Clean Architecture ve SOLID prensipleri uygulanmaya �al���lm��t�r.</br>

> Projenin amac� Basit bir e-ticaret sitesinin API katman�n�n olu�turulmas� ve kullan�c�ya �yelik durumlar� ile ilgili
Bilgilendirme yapabilmektir.</br>

> Kullan�lan teknolojiler ve yap�lar; <b>EntityFramework, Generic repository pattern, 
UnitOfWork, Service yap�lar�, Hangfire bakground jobs, JWT ve Smtp mail entegrasyonu</b> kullan�lm��t�r.
<b>MSSQL</b>'de projenin gerekli t�m verileri "SodexoFinalProjectDb" ve sisteme entegre �al��an HangFire job'un verilerinin tutulmas� i�in 
"SodexoFinalProjectHF" ad�nda 2 ayr� Database olu�turulmu�tur.
ASP.NET Core Identity package entegre edilerek �yelik sistemi projeye dahil edilmi�tir ve kullan�c�lar sisteme �nce kay�t olmal�d�r ve 
sonras�nda login olmadan uygulamay� kullanamazlar.</br>
Kullan�c�lara belli roller atanarak eri�ilebilecek end pointler s�n�rland�r�lm��t�r.
Kullan�c�lar�n giri� yapt�klar�nda kendilerine bir g�n s�re ile ge�erli olan bir token atan�r ve bu token ile rolleri dahilindeki end pointlere ula�abilir.

> Geli�tirmeler sa�lan�rken Code-First olarak ilerlenilmi�tir. Projeyi incelemek i�in 
indiren kullan�c�lar�n <b><u>Package manager console</u></b> 'dan <b><u>update-database</u></b> komutunu �al��t�rmas� yeterlidir.
Bu komut sonras�nda projenin Database i otomatik olarak olu�turulacakt�r.
HangFire'� �al��t�rmak i�in de MSSQL de <b>SodexoFinalProjectHF</b> ad�nda bir database olu�turulmal�d�r.
Solution Run edildi�inde otomatik olarak Admin Rolunde bir user sisteme kay�t edilmi� olacakt�r.
UserName ve parolas�na Infrastructure katman�nda ApplicationDbContextSeed s�n�f� i�inden ula��lailir.</br>
Kullan�c�lara sistemden ald��� verilerle otomatik bilgi maili g�nderen Smtp service aktif olarak �al��maktad�r.

## Databases
#### SodexoFinalProjectDb
<img src=""/>

#### SodexoFinalProjectHF
<img src=""/>

## Controllers
### <u>MemberOperationController</u>
Kullan�c�lar�n di�er API endpointlerine call yap�p verileri �ekebilmesi i�in �ncelikle bu controllerda POST/Register metoduyla
tek seferlik ba�ar�l� bir kay�t i�lemi ger�ekle�tirmesi gerekmektedir. 
��lem ba�ar�l� oldu�u taktirde bir bilgi mesaj� ve ki�iye �zel atanm�� Token d�nd�r�lerek uygulamaya Login edilmi� olur
ve kullan�c�ya asenkron �al��an Hangfire job yard�m� ile "Welcome" mesaj� i�eren bir mail iletilir.
Kullan�c� daha sonraki giri�lerinde sadece Email ve �ifresini kullanarak POST/Login metoduyla Login olmas� yeterlidir.
Login i�leminden sonra kullan�c�ya atanan Token bilgisiyle ve <b><u>Postman</u></b> yard�m� ile ula�maya yetkisi oldu�u butun endpointlere istek atabilir.
Kullan�c� uygulamaya e�er 3 kere hatal� giri� yapmay� denerse,<br>
sistem otamatik olarak kullan�c�y� 3 g�n s�re ile(deneme ama�l� 5dk olarak set edilmi�tir.) blocklamaktad�r ve
sistemde kay�tl� olan Email adresine Hangfire job yard�m� ile Blocklanma bilgisini i�eren bir mail iletilir.
Eger herhangi bir mail g�nderme i�lemi 5 denemede ba�ar�l� bir �ekilde iletilemezse kullan�c� unReachable olarak ba�ka bir statuye �ekilir

#### Register
<img src=""/>

<img src=""/>

#### Login
<img src=""/>

#### Block
<img src=""/>

<img src=""/>

### <u>ProductController</u>
Bu Controllerda Sisteme kay�tl� login olmu� ve Rolu sadece <b><u>admin</u></b> olan kullan�c�lar
Get/Product metodu ile o zamana kadar kullan�c�lar taraf�ndan olu�turulmu� b�t�n Product bilgilerine ula�abilirler.<br>
Rolu <b><u>member(admin olmayan)</u></b> olan kullan�c�lar
Get/Product/OnSale methodu ile sadece sat��ta olan b�t�n �r�nleri, GetById metodu ile sadece tek bir �r�n� g�rebilirler.
Kullan�c�lar Delete/Product/id ve Put/Product/update metodlar� ile sadece kendilerine ait olan �r�nleri silebilir ve g�ncelleyebilirler.
Put/Product/Buy metodu ile de offer vermeden de �r�nleri direk sat�n alabilirler.
Kullan�c�lar sadece kendi olu�turduklar� �r�nlerinin PictureUri lerini Patch/DeletePicture metodu ile sistemden silebilirler.

#### Postman Result Of Product
<img src=""/>

<img src=""/>

<img src=""/>

### <u>MyAccountController</u>
Bu Controllerda sisteme kay�tl� login olmu� kullan�c�lar Get/MyAccount/SendedOffer metodu ile �r�nlere g�nderdikleri offerlar�,
Get/MyAccount/RecivedOffer metodu ile de �r�nlerine ald�klar� offerlar� g�r�nt�leyebilirler. Ayr�ca Put/MyAccount/ReplyOffer ile
kendi productlar�na gelen offerlar� 0(Reddet) ve 1(onayla) ile de�erlendirebilirler.
Kullan�c�n�n product'�na gelen offer'� onaylanmas� durumunda product'�n fiyat� offer da istenilen indirim oran�na g�re d�zenlenir.
Kullan�c�lar Post/MyAccount/SendOffer ile herhangi bir �r�ne offer g�nderebilir ve Delete/MyAccount/WithDrawOffer metodu ile de offer�n� geri �ekebilir.

#### Postman Result Of MyAccount Controller

<img src=""/>

<img src=""/>

### <u>BrandController</u>
Bu Controllerda sisteme kay�tl� login olmu� b�t�n kullan�c�lar Get metodu ile b�t�n Brand bilgilerine ula�abilir.<br>
Ama yeni bir brand olu�turma veya silme i�lemine eri�im hakk� sadece Rolu admin olan kullan�c�larda vard�r.

#### Postman Result Of Brand Controller

<img src=""/>

### <u>CategoryController</u>
Bu Controllerda sisteme kay�tl� login olmu� b�t�n kullan�c�lar Get metodu ile b�t�n kategorileri g�r�nt�leyebilirler.
 > <b> <u>Ayr�ca GetById metodu ile kategoriyi ve i�indeki �r�nlerinde biligilerini g�r�nt�l�yebilirler.</u></b>

Ama yeni bir Category olu�turma veya silme i�lemine eri�im hakk� sadece Rolu admin olan kullan�c�da vard�r.
Sadece admin var olan bir kategoriyi Put/UpdateCategory metodu ile d�zenleyebilir.

#### Postman Result Of Category Controller

<img src=""/>

### <u>ColorController</u>
Bu Controllerda sisteme kay�tl� login olmu� b�t�n kullan�c�lar Get metodu ile b�t�n Color bilgilerine ula�abilirler.<br>
Ama yeni bir Color olu�turma veya silme i�lemine eri�im hakk� sadece Rolu admin olan kullan�c�da vard�r.

#### Postman Result Of Color Controller

<img src=""/>

### <u>ConditionsOfProductController</u>
Bu Controllerda sisteme kay�tl� login olmu� b�t�n kullan�c�lar Get metodu ile b�t�n ConditionsOfProduct bilgilerine ula�abilirler.<br>
Ama yeni bir ConditionsOfProduct olu�turma veya silme i�lemine eri�im hakk� sadece Rolu admin olan kullan�c�da vard�r.

#### Postman Result Of ConditionsOfProduct Controller

<img src=""/>


### HangFire 

<img src=""/>

<img src=""/>

<img src=""/>

### Swagger

<img src=""/>

### DTOs

<img src=""/>



