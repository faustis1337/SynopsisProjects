package com.dls.Repositories;

import com.dls.Entities.MessageRequest;

import java.util.ArrayList;
import java.util.List;

public class MessageRepository {
    private List<MessageRequest> messages = new ArrayList<>();

    public void add(MessageRequest messageRequest){
        messages.add(messageRequest);
    }

    public List<MessageRequest> getAllMessages(){
        return messages;
    }
}
