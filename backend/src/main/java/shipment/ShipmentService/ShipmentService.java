package ShipmentService;

import shipment.ShipmentDto.ShipmentDto;
import shipment.ShipmentRepository.ShipmentRepository;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;

public class ShipmentService {
    @Autowired
    private ShipmentRepository shipmentRepository;

    public List<ShipmentDto> getAllShipments () {
        //TODO: map entities -> DTOs
        return List.of(); // Array vazio por agora
    }
}
