using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class MateriaController : ApiController
    {
        [HttpGet]
        [Route("Api/Materia/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAllEF();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Api/Materia/Add")]
        public IHttpActionResult Add([FromBody]ML.Materia materia)
        {
            ML.Result result = BL.Materia.AddEF(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Api/Materia/Update")]
        public IHttpActionResult Update([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.UpdateEF(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("Api/Materia/GetById/{idMateria}")]
        public IHttpActionResult GetById(int idMateria)
        {
            ML.Result result = BL.Materia.GetByIdEF(idMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("Api/Materia/Delete/{idMateria}")]
        public IHttpActionResult Delete(int idMateria)
        {
            ML.Result result = BL.Materia.DeleteEF(idMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }


    }
}