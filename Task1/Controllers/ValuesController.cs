using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Task1.Controllers
{
    [Route("api/[Controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return BadRequest("Сервер успешно запушен");
        }


        [HttpGet("nearestPrime")]
        public ActionResult GetTask1([FromQuery] string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return BadRequest("Входной параметр может быть в пределах от 1 до 500 000 000 000");

            const long maxValue = 500000000000;

            if (long.TryParse(number, out var value))
            {
                if (value < 0 || value == 0 || value > maxValue) return BadRequest("Входной параметр может быть в пределах от 1 до 500 000 000 000");
                if (value == maxValue) return Ok(value);

                return Ok(NearestPrime(value));
            }
            else
                return BadRequest("Входной параметр может быть в пределах от 1 до 500 000 000 000");
        }

        private static long NearestPrime(double original)
        {
            var above = (long)Math.Ceiling(original);
            var below = (long)Math.Floor(original);

            if (above <= 2)
                return 2;

            if (below == 2)
                return (original - 2 < 0.5) ? 2 : 3;

            if (below % 2 == 0) below -= 1;
            if (above % 2 == 0) above += 1;

            double diffBelow = double.MaxValue, diffAbove = double.MaxValue;

            for (; ; above += 2, below -= 2)
            {
                if (IsPrime(below))
                    diffBelow = original - below;

                if (IsPrime(above))
                    diffAbove = above - original;

                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (diffAbove != double.MaxValue || diffBelow != double.MaxValue)
                    break;
            }

            return above;
        }

        private static bool IsPrime(long p)
        {
            for (var i = 3; i < Math.Sqrt(p); i += 2)
            {
                if (p % i == 0)
                    return false;
            }

            return true;
        }
    }
}
