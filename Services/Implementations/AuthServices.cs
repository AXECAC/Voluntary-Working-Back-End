using System.Diagnostics;
using Context;
using DataBase;
namespace Services;

// Класс AuthServices
public class AuthServices : IAuthServices
{
    private readonly IUserRepository _UserRepository;
    private readonly IHashingServices _HashingServices;
    private readonly ITokenServices _TokenServices;

    public AuthServices(IUserRepository userRepository, IHashingServices hashingServices,
            ITokenServices tokenServices)
    {
        _UserRepository = userRepository;
        _HashingServices = hashingServices;
        _TokenServices = tokenServices;
    }

    public async Task<IBaseResponse<Tokens>> TryRegister(User user, string secretKey)
    {
        // Хэширование Password
        user = _HashingServices.Hashing(user);

        BaseResponse<Tokens> baseResponse;

        //Найти User по email
        var userDb = await _UserRepository.FirstOrDefaultAsync(x => x.Email == user.Email);

        // Новый User
        if (userDb == null)
        {
            user.Id = 0;
            user.Role = "Student";
            user.Points = 0;
            user.FinishedRequests = 0;
            // Создать новый User
            await _UserRepository.Create(user);
            // Created (201)
            baseResponse = BaseResponse<Tokens>.Created(data: await _TokenServices.GenerateJWTToken(user, secretKey));
        }
        // Этот email уже существует
        else
        {
            // Conflict (409)
            baseResponse = BaseResponse<Tokens>.Conflict("This email already exists");
        }
        return baseResponse;

    }

    public async Task<IBaseResponse<Tokens>> TryLogin(LoginUser form, string secretKey)
    {
        
        BaseResponse<Tokens> baseResponse;
        // Найти user по email
        var userDb = await _UserRepository.FirstOrDefaultAsync(x => x.Email == form.Email);

        // Подсчет времени затраченного на проверку пароля
        var passwordVerifyTime = Stopwatch.StartNew();

        // User существует
        if (userDb != null)
        {
            // Сравнить хэш пароля
            if (_HashingServices.Verify(form.Password, userDb.Password))
            {
                passwordVerifyTime.Stop();
                // Ok (200)
                baseResponse = BaseResponse<Tokens>.Ok(data: await _TokenServices.GenerateJWTToken(userDb, secretKey));
            }
            else
            {
                passwordVerifyTime.Stop();
                // Unauthorized (401)
                baseResponse = BaseResponse<Tokens>.Unauthorized("Bad password");
            }
        }
        // User не существует
        else
        {
            // Фиктивная проверка
            _HashingServices.Verify(form.Password);

            passwordVerifyTime.Stop();
            // Unauthorized (401)
            baseResponse = BaseResponse<Tokens>.Unauthorized("Email not found");
        }


        // Создаем фиксированную задержку в 250 мс
        // Смотрим сколько осталось подождать до 250 мс
        var remainigDelay = TimeSpan.FromMilliseconds(250) - passwordVerifyTime.Elapsed;
        // Ждем оставшееся время
        if(remainigDelay > TimeSpan.Zero)
        {
            await Task.Delay(remainigDelay);
        }
        return baseResponse;
    }
}
