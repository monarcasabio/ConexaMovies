using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaMovies.Application.Dtos;

public sealed record MovieBriefResponse(
    int Id,
    string Title,
    string EpisodeId,
    string ReleaseDate);
