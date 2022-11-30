// See https://aka.ms/new-console-template for more information
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

Console.WriteLine("Hello, World!");

//JWT加密
List<Claim> claims = new List<Claim>();
claims.Add(new Claim("Passport", "123456"));
claims.Add(new Claim("QQ", "88888888"));
claims.Add(new Claim("Id", "0001"));
claims.Add(new Claim("UserName", "admin"));
claims.Add(new Claim(ClaimTypes.Name, "张三"));
var key = "asd123asd123asd132asd1as32d"; //密钥
DateTime expire = DateTime.Now.AddHours(1);
byte[] secBytes=Encoding.UTF8.GetBytes(key);
var secKey = new SymmetricSecurityKey(secBytes);
var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
var token = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: credentials);
var  jwt=new JwtSecurityTokenHandler().WriteToken(token);
Console.WriteLine("生成的jwt:"+jwt);

//解码JWT
JwtSecurityTokenHandler JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
TokenValidationParameters valParam = new();
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
valParam.IssuerSigningKey = securityKey;
valParam.ValidateIssuer = false;
valParam.ValidateAudience = false;
ClaimsPrincipal claimsPrincipal = JwtSecurityTokenHandler.ValidateToken(jwt, valParam, out var secToken);
foreach (var item in claimsPrincipal.Claims)
{
    Console.WriteLine($"{item.Type}={item.Value}");
}


