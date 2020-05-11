
#include <vector>
#include <string>
#include <unordered_set>
#include <limits>
#include <algorithm>
#include "../include/User.h"
#include "../include/Session.h"
#include "../include/Watchable.h"

using string=std::string;
using vectorSt= std::vector<string>;

User::User(const std::string &name):
        history(std::vector<Watchable*>()),name(name)
{}


std::string User::getName()const
{
    return name;
}
std::vector<Watchable*> User::get_history() const
{
    return history;
}

User *User::Copy(const User& other ) {
    std::vector<Watchable*>::const_iterator ptr = other.history.begin();
    for (;ptr< other.history.end();ptr++)
    {
        history.push_back((*ptr)->GetCopy());
    }
    return this;
}

void User::watchContent(Watchable* w) {
    std::vector<Watchable*>::iterator ptr = history.begin();
    bool found= false;
    for(;ptr<history.end() && !found;++ptr)
    {
        if((*ptr)->getId()==w->getId())
        {
            found=true;
        }
    }
    if(!found)
        history.push_back(w->GetCopy());
}

GenreRecommenderUser* GenreRecommenderUser::GetCopy(const std::string &name)
{
    GenreRecommenderUser* temp = new GenreRecommenderUser(name);
    temp->Copy(*this);
    return temp;
}


RerunRecommenderUser* RerunRecommenderUser::GetCopy(const std::string &name)
{
    RerunRecommenderUser* temp = new RerunRecommenderUser(name);
    temp->Copy(*this);
    return temp;
}


LengthRecommenderUser* LengthRecommenderUser::GetCopy(const std::string &name) {
    LengthRecommenderUser* temp = new LengthRecommenderUser(name);
    temp->Copy(*this);
    return temp;
}
//LengthRecommenderUser
LengthRecommenderUser::LengthRecommenderUser(const std::string &name) : User(name) {}

Watchable *LengthRecommenderUser::getRecommendation(Session &s) {
    std::vector<Watchable*>* NotSeen = UnWatched(s);//**do it more efficiently
    int avg =GetHistoAvg();
    int opt=std::numeric_limits<int>::max();//initialize output with infinity
    Watchable* output;
    std::vector<Watchable*>::iterator ptr= NotSeen->begin();
    for(;ptr<NotSeen->end();++ptr) {
        int diff = (*ptr)->getLength() - avg;
        diff = abs(diff);//Math abs
        if (opt > diff) {
            opt = diff;
            output = *ptr;
        }
    }
    delete NotSeen;
    return output;
}


//Rerun AlgorithmRerunIndex=0
RerunRecommenderUser::RerunRecommenderUser(const std::string &name) : User(name), RerunIndex(0) {}

Watchable *RerunRecommenderUser::getRecommendation(Session &s) {
    Watchable* output=history.at(RerunIndex);
    RerunIndex=(RerunIndex+1)%(history.size());
    return output;
}
RerunRecommenderUser::~RerunRecommenderUser() {

}


struct myPair
{
    int first;
    string second;
    myPair(const int& v,const string& s):first(v),second(s){}
};
bool myComp(const myPair& lhs, const myPair& rhs)
{
    if(lhs.first<rhs.first)
        return true;
    else if(lhs.first>rhs.first)
        return false;
    else{
        return lhs.second.compare(rhs.second);
    }
}
Watchable *GenreRecommenderUser::getRecommendation(Session &s) {


    // find most popular tags in users history
    std::vector<myPair> * popTags = new std::vector<myPair>;
    std::map<std::string,int>::iterator ptrCount = tagsCount->begin();
    while (ptrCount != tagsCount->end())
    {
        popTags->push_back(myPair(ptrCount->second, ptrCount->first));
        ptrCount++;
    }
    std::sort(popTags->begin(),popTags->end(),myComp);

    //choose most popular tag
    //std::vector<myPair>::reverse_iterator ptrPop = popTags->rbegin();
    auto ptrPop = popTags->rbegin();
    std::vector<Watchable*> * content= UnWatched(s);
    std::vector<Watchable*>::iterator  p;
    bool found=false;
    Watchable* output;
    for(;(ptrPop<(popTags->rend())) && !found;++ptrPop)
    {
        string tagName =ptrPop->second;

        // find first unwatched content with same tag
        for (p=(*content).begin();(p<(*content).end())&&(!found);p++)
        {
            vectorSt tags = (*p)->getTags();
            auto tagItr= tags.begin();
            for(; (tagItr < tags.end()) && !found; tagItr++)
            {
                if(tagName==*tagItr)
                {
                    found=true;
                    output =*p;
                }
            }
        }
    }
    delete popTags;
    popTags=nullptr;
    delete content;
    content=nullptr;
    if(found)
        return output;
    else
        return 0;
}

void GenreRecommenderUser::watchContent(Watchable *w) {
    User::watchContent(w);
    std::vector<std::string> tags = w->getTags();
    std::vector<std::string>::iterator ptr;
    for (ptr = tags.begin(); ptr < tags.end(); ptr++)
    {
        try {
            tagsCount->at(*ptr)++;
        }
        catch(const std::out_of_range& oor)
        {
            tagsCount->insert({*ptr,1});
        }
    }
}

//GenreRecommenderUser
GenreRecommenderUser::GenreRecommenderUser(const std::string &name) : User(name),
                              tagsCount(new  std::map<std::string,int>()) {}

//destructor
GenreRecommenderUser::~GenreRecommenderUser(){
    Clean();
}

//copy assignment operator
GenreRecommenderUser &GenreRecommenderUser::operator=(const GenreRecommenderUser &other) {
    if (this != &other){  //check for "self assignment"
        Copy(other);
    }
    return *this;
}
//cop constructor
GenreRecommenderUser::GenreRecommenderUser(const GenreRecommenderUser & other):
    User(other.getName()),tagsCount(new std::map<std::string,int>)
{
    Copy(other);
}

void GenreRecommenderUser::Copy(const GenreRecommenderUser & other) {
    User::Copy(other);
    tagsCount->clear();
    auto ptr = (*other.tagsCount).begin();
    for (;ptr!=(*other.tagsCount).end();++ptr)
    {
        tagsCount->insert({(*ptr).first,(*ptr).second});
    }
}

void GenreRecommenderUser::Clean() {
    delete tagsCount;
    tagsCount = nullptr;
}


//
//aditional

int LengthRecommenderUser::GetHistoAvg() {
    int sum=0;
    int i=0;
    std::vector<Watchable*>::iterator ptr = history.begin();
    for(;ptr<history.end();++ptr)
    {
        sum+=(*ptr)->getLength();
        i++;
    }
    return sum/i;
}

LengthRecommenderUser::~LengthRecommenderUser() {

}


std::vector<Watchable*>* User::UnWatched(Session& s) {
    std::vector<Watchable *> *output = new std::vector<Watchable*>;
    std::vector<Watchable*> content = s.getContent();
    std::vector<Watchable*>::iterator ptrContent = content.begin();
    for(;ptrContent != content.end();++ptrContent)
    {
        bool watchableFound = false;
        std::vector<Watchable*>::iterator ptrHist = history.begin();
        for(;(ptrHist!=history.end()) && !watchableFound;++ptrHist)
        {
            if((*ptrContent)->getId() == (*ptrHist)->getId())
            {
                watchableFound=true;
            }
        }
        if(!watchableFound)
        {
            output->push_back(*ptrContent);
        }
    }
    return output;
}

User::~User() {
    auto ptr = history.begin();
    for (;ptr!=history.end();++ptr)
    {
        delete *ptr;
    }
    history.clear();
}

