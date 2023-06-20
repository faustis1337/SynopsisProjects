package com.dls;

import com.dls.Entities.MessageRequest;
import com.dls.Repositories.MessageRepository;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

@SpringBootApplication
@RestController
@RequestMapping("/api/messages")
public class MessagesController {

    private final MessageRepository messageRepository = new MessageRepository();
    private static final Logger logger = LoggerFactory.getLogger(MessagesController.class);

    public static void main(String[] args) {
        SpringApplication.run(MessagesController.class, args);
    }

    @PostMapping
    public MessageRequest newMessage(@RequestBody MessageRequest request) {
        logger.info("Creating a new message");
        String id = request.getId();
        String message = request.getMessage();
        if (id != null && message != null) {
            logger.info("Storing message");
            messageRepository.add(request);
        }
        return request;
    }

    @GetMapping
    public List<MessageRequest> getAllMessages() {
        logger.info("Getting all messages");
        return messageRepository.getAllMessages();
    }
}
