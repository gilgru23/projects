#include "../include/Watchable.h"
#include "../include/Session.h"

Watchable::Watchable(long id, int length, const std::vector<std::string> &tags):
    id(id),length(length),tags(tags){}

int Watchable::getLength() const {
    return length;


}

long Watchable::getId() const {
    return id;
}

const std::vector<std::string>& Watchable::getTags() {
    return tags;
}

Watchable::~Watchable() {
 tags.clear();
}

Watchable *Watchable::Copy() {
    return this->GetCopy();

}

//MOVIE
Movie::Movie(long id, const std::string& name, int length, const std::vector<std::string>& tags):
        Watchable(id,length,tags),name(name) {}

std::string Movie::toString() const
{
    return name;
}

Watchable *Movie::getNextWatchable(Session &s) const {
    return s.GetActiveUser()->getRecommendation(s);
}

Watchable* Movie::GetCopy() {
    return new Movie(getId(),name,getLength(),getTags());
}
//Episode
Episode::Episode(long id, const std::string& seriesName, int length, int season, int episode , const std::vector<std::string>& tags):
        Watchable(id,length,tags),seriesName(seriesName),season(season),
        episode(episode),nextEpisodeId(id+1){}


std::string Episode::toString() const
{
    std::string s = std::to_string(season);
    if(season<10)
        s="0"+s;
    std::string e = std::to_string(episode);
    if(episode<10)
        e="0"+e;
    return seriesName+" S"+s+"E"+e;
}
Watchable* Episode::getNextWatchable(Session &s) const {
    if (nextEpisodeId==-1)
        return s.GetActiveUser()->getRecommendation(s);
    else
    {
        return s.getContent().at(nextEpisodeId);
    }
}

void Episode::SetNextEpId(int i) {
    nextEpisodeId=i;
}

Watchable *Episode::GetCopy() {
    return new Episode(getId(),seriesName,getLength(),season,episode,getTags());
}





