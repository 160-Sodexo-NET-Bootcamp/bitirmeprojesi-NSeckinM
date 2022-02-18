# Sodexo Final Project

> Tamamladýðým Bu WebAPI projesinde Clean Architecture ve SOLID prensipleri uygulanmaya çalýþýlmýþtýr.</br>

> Projenin amacý Basit bir e-ticaret sitesinin API katmanýnýn oluþturulmasý ve kullanýcýya üyelik durumlarý ile ilgili
Bilgilendirme yapabilmektir.</br>

> Kullanýlan teknolojiler ve yapýlar; <b>EntityFramework, Generic repository pattern, 
UnitOfWork, Service yapýlarý, Hangfire bakground jobs, JWT ve Smtp mail entegrasyonu</b> kullanýlmýþtýr.
<b>MSSQL</b>'de projenin gerekli tüm verileri "SodexoFinalProjectDb" ve sisteme entegre çalýþan HangFire job'un verilerinin tutulmasý için 
"SodexoFinalProjectHF" adýnda 2 ayrý Database oluþturulmuþtur.
ASP.NET Core Identity package entegre edilerek Üyelik sistemi projeye dahil edilmiþtir ve kullanýcýlar sisteme önce kayýt olmalýdýr ve 
sonrasýnda login olmadan uygulamayý kullanamazlar.</br>
Kullanýcýlara belli roller atanarak eriþilebilecek end pointler sýnýrlandýrýlmýþtýr.
Kullanýcýlarýn giriþ yaptýklarýnda kendilerine bir gün süre ile geçerli olan bir token atanýr ve bu token ile rolleri dahilindeki end pointlere ulaþabilir.

> Geliþtirmeler saðlanýrken Code-First olarak ilerlenilmiþtir. Projeyi incelemek için 
indiren kullanýcýlarýn <b><u>Package manager console</u></b> 'dan <b><u>update-database</u></b> komutunu çalýþtýrmasý yeterlidir.
Bu komut sonrasýnda projenin Database i otomatik olarak oluþturulacaktýr.
HangFire'ý çalýþtýrmak için de MSSQL de <b>SodexoFinalProjectHF</b> adýnda bir database oluþturulmalýdýr.
Solution Run edildiðinde otomatik olarak Admin Rolunde bir user sisteme kayýt edilmiþ olacaktýr.
UserName ve parolasýna Infrastructure katmanýnda ApplicationDbContextSeed sýnýfý içinden ulaþýlailir.</br>
Kullanýcýlara sistemden aldýðý verilerle otomatik bilgi maili gönderen Smtp service aktif olarak çalýþmaktadýr.

## Databases
#### SodexoFinalProjectDb
<img src=""/>

#### SodexoFinalProjectHF
<img src=""/>

## Controllers
### <u>MemberOperationController</u>
Kullanýcýlarýn diðer API endpointlerine call yapýp verileri çekebilmesi için öncelikle bu controllerda POST/Register metoduyla
tek seferlik baþarýlý bir kayýt iþlemi gerçekleþtirmesi gerekmektedir. 
Ýþlem baþarýlý olduðu taktirde bir bilgi mesajý ve kiþiye özel atanmýþ Token döndürülerek uygulamaya Login edilmiþ olur
ve kullanýcýya asenkron çalýþan Hangfire job yardýmý ile "Welcome" mesajý içeren bir mail iletilir.
Kullanýcý daha sonraki giriþlerinde sadece Email ve þifresini kullanarak POST/Login metoduyla Login olmasý yeterlidir.
Login iþleminden sonra kullanýcýya atanan Token bilgisiyle ve <b><u>Postman</u></b> yardýmý ile ulaþmaya yetkisi olduðu butun endpointlere istek atabilir.
Kullanýcý uygulamaya eðer 3 kere hatalý giriþ yapmayý denerse,<br>
sistem otamatik olarak kullanýcýyý 3 gün süre ile(deneme amaçlý 5dk olarak set edilmiþtir.) blocklamaktadýr ve
sistemde kayýtlý olan Email adresine Hangfire job yardýmý ile Blocklanma bilgisini içeren bir mail iletilir.
Eger herhangi bir mail gönderme iþlemi 5 denemede baþarýlý bir þekilde iletilemezse kullanýcý unReachable olarak baþka bir statuye çekilir

#### Register
<img src=""/>

<img src=""/>

#### Login
<img src=""/>

#### Block
<img src=""/>

<img src=""/>

### <u>ProductController</u>
Bu Controllerda Sisteme kayýtlý login olmuþ ve Rolu sadece <b><u>admin</u></b> olan kullanýcýlar
Get/Product metodu ile o zamana kadar kullanýcýlar tarafýndan oluþturulmuþ bütün Product bilgilerine ulaþabilirler.<br>
Rolu <b><u>member(admin olmayan)</u></b> olan kullanýcýlar
Get/Product/OnSale methodu ile sadece satýþta olan bütün ürünleri, GetById metodu ile sadece tek bir ürünü görebilirler.
Kullanýcýlar Delete/Product/id ve Put/Product/update metodlarý ile sadece kendilerine ait olan ürünleri silebilir ve güncelleyebilirler.
Put/Product/Buy metodu ile de offer vermeden de ürünleri direk satýn alabilirler.
Kullanýcýlar sadece kendi oluþturduklarý ürünlerinin PictureUri lerini Patch/DeletePicture metodu ile sistemden silebilirler.

#### Postman Result Of Product
<img src=""/>

<img src=""/>

<img src=""/>

### <u>MyAccountController</u>
Bu Controllerda sisteme kayýtlý login olmuþ kullanýcýlar Get/MyAccount/SendedOffer metodu ile ürünlere gönderdikleri offerlarý,
Get/MyAccount/RecivedOffer metodu ile de ürünlerine aldýklarý offerlarý görüntüleyebilirler. Ayrýca Put/MyAccount/ReplyOffer ile
kendi productlarýna gelen offerlarý 0(Reddet) ve 1(onayla) ile deðerlendirebilirler.
Kullanýcýnýn product'ýna gelen offer'ý onaylanmasý durumunda product'ýn fiyatý offer da istenilen indirim oranýna göre düzenlenir.
Kullanýcýlar Post/MyAccount/SendOffer ile herhangi bir ürüne offer gönderebilir ve Delete/MyAccount/WithDrawOffer metodu ile de offerýný geri çekebilir.

#### Postman Result Of MyAccount Controller

<img src=""/>

<img src=""/>

### <u>BrandController</u>
Bu Controllerda sisteme kayýtlý login olmuþ bütün kullanýcýlar Get metodu ile bütün Brand bilgilerine ulaþabilir.<br>
Ama yeni bir brand oluþturma veya silme iþlemine eriþim hakký sadece Rolu admin olan kullanýcýlarda vardýr.

#### Postman Result Of Brand Controller

<img src=""/>

### <u>CategoryController</u>
Bu Controllerda sisteme kayýtlý login olmuþ bütün kullanýcýlar Get metodu ile bütün kategorileri görüntüleyebilirler.
 > <b> <u>Ayrýca GetById metodu ile kategoriyi ve içindeki ürünlerinde biligilerini görüntülüyebilirler.</u></b>

Ama yeni bir Category oluþturma veya silme iþlemine eriþim hakký sadece Rolu admin olan kullanýcýda vardýr.
Sadece admin var olan bir kategoriyi Put/UpdateCategory metodu ile düzenleyebilir.

#### Postman Result Of Category Controller

<img src=""/>

### <u>ColorController</u>
Bu Controllerda sisteme kayýtlý login olmuþ bütün kullanýcýlar Get metodu ile bütün Color bilgilerine ulaþabilirler.<br>
Ama yeni bir Color oluþturma veya silme iþlemine eriþim hakký sadece Rolu admin olan kullanýcýda vardýr.

#### Postman Result Of Color Controller

<img src=""/>

### <u>ConditionsOfProductController</u>
Bu Controllerda sisteme kayýtlý login olmuþ bütün kullanýcýlar Get metodu ile bütün ConditionsOfProduct bilgilerine ulaþabilirler.<br>
Ama yeni bir ConditionsOfProduct oluþturma veya silme iþlemine eriþim hakký sadece Rolu admin olan kullanýcýda vardýr.

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



