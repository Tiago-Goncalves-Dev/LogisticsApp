package ShipmentRepository;

import shipment.entity.Shipment;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ShipmentRepository  extends JpaRepository<Shipment, Long> {
    // Sem código = já tem findAll(), save(), etc!
    // Spring gere Bean automaticamente
    // interface para escalabilidade e compatibilidade com o Spring

}
