<template>
  <div>
    <service-selector
      ref="ServiceSelector"
      v-model="value.service"
      v-show="step == 1"
      @done="step = 2"
    />
    <doctor-selector
      ref="DoctorSelector"
      :serviceID="value.service.serviceID"
      v-model="value.doctor"
      v-show="step == 2"
      @back="step = 1"
      @done="done"
    />
  </div>
</template>

<script>
import DoctorSelector from "./DoctorSelector.vue";
import ServiceSelector from "./ServiceSelector.vue";

export default {
  components: { ServiceSelector, DoctorSelector },
  props: {
    value: {
      type: Object,
      default: function() {
        return {
          service: {
            serviceID: "",
            serviceName: "",
          },
          doctor: {
            userID: "",
            fullName: "",
          },
        };
      },
    },
  },
  data() {
    return {
      step: 1,
    };
  },
  mounted() {},
  methods: {
    clean() {
      this.step = 1;
      this.value.service = JSON.parse(
        JSON.stringify({
          serviceID: "",
          serviceName: "",
        })
      );
      this.value.doctor = JSON.parse(
        JSON.stringify({
          userID: "",
          fullName: "",
        })
      );
    },
    done() {
      this.step = 1;
      this.$emit("input", this.value);
      this.$emit("done");
    },
  },
};
</script>
