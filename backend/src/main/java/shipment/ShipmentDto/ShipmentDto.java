package ShipmentDto;

import shipment.entity.ShipmentStatus;
import java.time.LocalDateTime;

public class ShipmentDto { // Sem @Entity!
    private Long id;
    private String origin;
    private String destination;
    private ShipmentStatus status;
    private LocalDateTime createdAt;

    public ShipmentDto() {} //Construtor vazio, obrigatorio

    //Construtor para mapear ( uso posterior )
    public ShipmentDto(Long id, String origin, String destination, ShipmentStatus status, LocalDateTime createdAt)
    {
        this.id = id;
        this.origin = origin;
        this.destination = destination;
        this.status = status;
        this.createdAt = createdAt;
    }
}
