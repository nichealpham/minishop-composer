<template>
  <div>
    <asset-consumes
      v-show="show == 1"
      ref="AssetConsumes"
      :clinicID="clinicID"
      :episodeID="episodeID"
      :serviceID="serviceID"
      :editable="editable"
      @add="show = 2"
      @delete="$refs.AssetSelector.search()"
      @success="$refs.AssetSelector.search()"
    />
    <asset-selector
      v-show="show == 2"
      ref="AssetSelector"
      :clinicID="clinicID"
      :episodeID="episodeID"
      :serviceID="serviceID"
      @back="show = 1"
      @success="$refs.AssetConsumes.search()"
    />
  </div>
</template>

<script>
import AssetConsumes from "./AssetConsumes.vue";
import AssetSelector from "./AssetSelector.vue";
export default {
  components: { AssetSelector, AssetConsumes },
  props: ["clinicID", "episodeID", "serviceID", "editable"],
  data() {
    return {
      show: 1,
    };
  },
  methods: {
    clean() {
      this.show = 1;
    },
  },
};
</script>
