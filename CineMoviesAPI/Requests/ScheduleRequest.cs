using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

public abstract class ScheduleRequest
{
	public static Schedule Create(dynamic body)
	{
		return new Schedule
		{
			CinemaId = body.cinemaId,
			MovieId = body.movieId,
			FromTime = body.fromTime,
			ToTime = body.toTime
		};
	}

	public static Schedule Read(dynamic body)
	{
		return new Schedule
		{
			Id = body.id
		};
	}

	public static Schedule Update(dynamic body)
	{
		return new Schedule
		{
			Id = body.id,
			CinemaId = body.cinemaId,
			MovieId = body.movieId,
			FromTime = body.fromTime,
			ToTime = body.toTime
		};
	}

	public static Schedule Delete(dynamic body)
	{
		return new Schedule
		{
			Id = body.id
		};
	}
}
